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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.API.Products
{
    /// <summary>
    /// Controller to manage product actions
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ICreateProductHandler _createProductHandler;
        private readonly IGetProductByNameHandler _getProductByNameHandler;
        private readonly IRemoveProductByNameHandler _removeProductByNameHandler;
        private readonly IGetAllProductsHandler _getAllProductsHandler;

        /// <summary>
        /// Creates an instance of the ProductController class
        /// </summary>
        /// <param name="createProductUseCase">Product creation use case</param>
        /// <param name="getProductByNameHandler">Product retrieval by name use case</param>
        /// <param name="removeProductByNameHandler">Product deletion use case</param>
        /// <param name="getAllProductsHandler">Product retrieval use case</param>
        public ProductController(
            ICreateProductHandler createProductUseCase,
            IGetProductByNameHandler getProductByNameHandler,
            IRemoveProductByNameHandler removeProductByNameHandler,
            IGetAllProductsHandler getAllProductsHandler)
        {
            _createProductHandler = createProductUseCase ?? throw new ArgumentNullException(nameof(createProductUseCase));
            _getProductByNameHandler = getProductByNameHandler ?? throw new ArgumentNullException(nameof(getProductByNameHandler));
            _removeProductByNameHandler = removeProductByNameHandler ?? throw new ArgumentNullException(nameof(removeProductByNameHandler));
            _getAllProductsHandler = getAllProductsHandler ?? throw new ArgumentNullException(nameof(getAllProductsHandler));
        }

        /// <summary>
        /// Creates a product
        /// </summary>
        /// <param name="productDto">The product to create</param>
        /// <returns>A code to indicate success</returns>
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

            await _createProductHandler.Handle(createProductRequest);

            return CreatedAtAction(nameof(GetProductByNameAsync), new { name = productDto.Name }, null);
        }

        /// <summary>
        /// Deletes a product by name
        /// </summary>
        /// <param name="name">The name of the product to delete</param>
        /// <returns>Status code to indicate success</returns>
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

            return Ok(new ProductViewModel(response.Product));
        }

        /// <summary>
        /// Gets a product by name
        /// </summary>
        /// <param name="name">The name of the product to get</param>
        /// <returns>The product if exists, null otherwise</returns>
        [HttpGet("{name}")]
        [ProducesResponseType(typeof(CreateProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductViewModel>> GetProductByNameAsync(string? name)
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
                return Ok(new ProductViewModel(response.Product));
            }
        }

        /// <summary>
        /// Gets all products
        /// </summary>
        /// <returns>All the products in the database or an empty list if none</returns>
        [HttpGet]
        [ProducesResponseType(typeof(CreateProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> GetAllProductsAsync()
        {
            var response = await _getAllProductsHandler.Handle(new GetAllProductsRequest());

            return Ok(response.Products.Select(product => new ProductViewModel(product)));
        }
    }
}