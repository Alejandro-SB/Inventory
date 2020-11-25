using Inventory.API.Products.CreateProduct;
using Inventory.API.Products.GetProduct;
using Inventory.Application.Products.CreateProduct;
using Inventory.Application.Products.GetAllProducts;
using Inventory.Application.Products.GetByName;
using Inventory.Application.Products.RemoveByName;
using Inventory.Domain.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.API.Products
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly ICreateProductHandler _createProductHandler;
        private readonly IGetProductByNameHandler _getProductByNameHandler;
        private readonly IRemoveProductByNameHandler _removeProductByNameHandler;
        private readonly IGetAllProductsHandler _getAllProductsHandler;

        public ProductController(
            ILogger<ProductController> logger,
            ICreateProductHandler createProductUseCase,
            IGetProductByNameHandler getProductByNameHandler,
            IRemoveProductByNameHandler removeProductByNameHandler,
            IGetAllProductsHandler getAllProductsHandler)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _createProductHandler = createProductUseCase ?? throw new ArgumentNullException(nameof(createProductUseCase));
            _getProductByNameHandler = getProductByNameHandler ?? throw new ArgumentNullException(nameof(getProductByNameHandler));
            _removeProductByNameHandler = removeProductByNameHandler ?? throw new ArgumentNullException(nameof(removeProductByNameHandler));
            _getAllProductsHandler = getAllProductsHandler ?? throw new ArgumentNullException(nameof(getAllProductsHandler));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> CreateProductAsync([FromBody] CreateProductDto productDto)
        {
            var createProductRequest = new CreateProductRequest(productDto.Name!)
            {
                ProductType = productDto.ProductType,
                ExpirationDate = productDto.ExpirationDate
            };

            var response = await _createProductHandler.Handle(createProductRequest);

            return CreatedAtAction(nameof(GetProductByNameAsync), new { name = productDto.Name }, null);
        }

        [HttpDelete("{name}")]
        [ProducesResponseType(typeof(CreateProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CreateProductDto>> RemoveByNameAsync(string? name)
        {
            if (name.IsNullOrEmpty())
            {
                return BadRequest("Name cannot be empty");
            }

            var response = await _removeProductByNameHandler.Handle(new RemoveProductByNameRequest(name));

            return Ok(new ProductResponse(response.Product));
        }

        [HttpGet("{name}")]
        [ProducesResponseType(typeof(CreateProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductResponse>> GetProductByNameAsync(string? name)
        {
            if (name.IsNullOrEmpty())
            {
                return BadRequest("Name cannot be empty");
            }

            var response = await _getProductByNameHandler.Handle(new GetProductByNameRequest(name));

            if (response.Product is null)
            {
                return NotFound();
            }
            else
            {
                return Ok(new ProductResponse(response.Product));
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(CreateProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAllProductsAsync()
        {
            var response = await _getAllProductsHandler.Handle(new GetAllProductsRequest());

            return Ok(response.Products.Select(product => new ProductResponse(product)));
        }
    }
}