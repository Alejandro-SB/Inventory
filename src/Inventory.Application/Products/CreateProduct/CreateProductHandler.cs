using Inventory.Domain.DateTimeProvider;
using Inventory.Domain.Entities;
using Inventory.Domain.Persistence;
using Inventory.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Application.Products.CreateProduct
{
    public class CreateProductHandler : ICreateProductHandler
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CreateProductHandler(IProductRepository productRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider
            )
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        }

        public async Task<CreateProductResponse> Handle(CreateProductRequest request, CancellationToken cancellationToken = default)
        {
            string productName = request.Name;

            var existingProduct = await _productRepository.GetByNameAsync(productName);

            if (existingProduct is Product)
            {
                throw new ProductAlreadyExistsException(productName);
            }

            var product = new Product(request.Name)
            {
                ExpirationDate = request.ExpirationDate,
                ProductType = request.ProductType,
                DepositDate = _dateTimeProvider.UtcNow
            };

            _productRepository.AddProduct(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new CreateProductResponse(product.Id);
        }
    }
}