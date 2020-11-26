using Inventory.Application.Products.Dto;
using Inventory.Domain.Repositories;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Application.Products.GetAllProducts
{
    public class GetAllProductsHandler : IGetAllProductsHandler
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<GetAllProductsResponse> Handle(GetAllProductsRequest request, CancellationToken cancellationToken = default)
        {
            var products = await _productRepository.GetAllProducts();

            return new GetAllProductsResponse(products.Select(product => new ProductDto(product)));
        }
    }
}