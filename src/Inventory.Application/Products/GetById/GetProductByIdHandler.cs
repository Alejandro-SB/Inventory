using Inventory.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Application.Products.GetById
{
    public class GetProductByIdHandler : IGetProductByIdHandler
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<GetProductByIdResponse> Handle(GetProductByIdRequest request, CancellationToken cancellationToken = default)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);

            return new GetProductByIdResponse(product);
        }
    }
}