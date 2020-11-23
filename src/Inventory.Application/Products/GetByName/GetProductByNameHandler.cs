using Inventory.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
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

            return new GetProductByNameResponse(product);
        }
    }
}