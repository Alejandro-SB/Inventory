using Inventory.Domain.DateTimeProvider;
using Inventory.Domain.Persistence;
using Inventory.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Application.Products.RemoveByName
{
    public class RemoveProductByNameHandler : IRemoveProductByNameHandler
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveProductByNameHandler(IProductRepository productRepository, 
            IUnitOfWork unitOfWork
            )
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<RemoveProductByNameResponse> Handle(RemoveProductByNameRequest request, CancellationToken cancellationToken = default)
        {
            var product = await _productRepository.GetByNameAsync(request.Name);

            if(product is null)
            {
                throw new ProductNotFoundException(request.Name);
            }

            _productRepository.DeleteProduct(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new RemoveProductByNameResponse(product);
        }
    }
}