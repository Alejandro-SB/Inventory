using Inventory.API.Products.CreateProduct;
using Inventory.Application.Products;
using Inventory.Application.Products.CreateProduct;
using Inventory.Application.Products.GetById;
using Inventory.Application.Products.WithdrawByName;
using Inventory.Domain.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        private readonly IGetProductByIdHandler _getProductByIdHandler;
        private readonly IWithdrawProductByNameHandler _getProductByNameHandler;

        public ProductController(
            ILogger<ProductController> logger,
            ICreateProductHandler createProductUseCase,
            IGetProductByIdHandler getProductByIdHandler,
            IWithdrawProductByNameHandler getProductByNameHandler
            )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _createProductHandler = createProductUseCase ?? throw new ArgumentNullException(nameof(createProductUseCase));
            _getProductByIdHandler = getProductByIdHandler ?? throw new ArgumentNullException(nameof(getProductByIdHandler));
            _getProductByNameHandler = getProductByNameHandler ?? throw new ArgumentNullException(nameof(getProductByNameHandler));
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

            return CreatedAtAction(nameof(GetProductByIdAsync), new { id = response.Id }, null);
        }

        [HttpDelete("{name}")]
        [ProducesResponseType(typeof(CreateProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CreateProductDto>> WithdrawByNameAsync(string? name)
        {
            if (name.IsNullOrEmpty())
            {
                return BadRequest("Name cannot be empty");
            }

            var response = await _getProductByNameHandler.Handle(new WithdrawProductByNameRequest(name));

            return Ok(response.Product);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(CreateProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CreateProductDto>> GetProductByIdAsync(int id)
        {
            if (id < 1)
            {
                return BadRequest("Invalid id");
            }

            var response = await _getProductByIdHandler.Handle(new GetProductByIdRequest(id));

            if (response.Product is null)
            {
                return NotFound();
            }
            else
            {
                return Ok(response.Product);
            }
        }
    }
}