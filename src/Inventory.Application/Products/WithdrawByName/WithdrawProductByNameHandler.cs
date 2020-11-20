using Inventory.Domain.DateTimeProvider;
using Inventory.Domain.Persistence;
using Inventory.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Application.Products.WithdrawByName
{
    public class WithdrawProductByNameHandler : IWithdrawProductByNameHandler
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public WithdrawProductByNameHandler(IProductRepository productRepository, 
            IUnitOfWork unitOfWork
            )
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<WithdrawProductByNameResponse> Handle(WithdrawProductByNameRequest request, CancellationToken cancellationToken = default)
        {
            var product = await _productRepository.GetActiveByNameAsync(request.Name);

            if(product is null)
            {
                throw new ProductNotFoundException(request.Name);
            }

            _productRepository.DeleteProduct(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new WithdrawProductByNameResponse(product);
        }
    }
}