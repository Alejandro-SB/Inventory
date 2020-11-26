using Inventory.Application.Products.Dto;
using Inventory.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Application.Products.GetByName
{
    public class GetProductByNAmeHandler : IGetProductByNameHandler
    {
        private readonly IProductRepository _productRepository;

        public GetProductByNAmeHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<GetProductByNameResponse> Handle(GetProductByNameRequest request, CancellationToken cancellationToken = default)
        {
            var product = await _productRepository.GetByNameAsync(request.Name);

            var dto = product is null ? null : new ProductDto(product);

            return new GetProductByNameResponse(dto);
        }
    }
}