using Inventory.Domain.DateTimeProvider;
using Inventory.Domain.Events;
using Inventory.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Application.Products.NotifyExpiredProducts
{
    public class NotifyExpiredProductsHandler : INotifyExpiredProductsHandler
    {
        private readonly IEventBus _eventBus;
        private readonly IProductRepository _productRepository;

        public NotifyExpiredProductsHandler(IProductRepository productRepository, IEventBus eventBus)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        public async Task<NotifyExpiredProductsResponse> Handle(NotifyExpiredProductsRequest request, CancellationToken cancellationToken = default)
        {
            var expiredProducts = await _productRepository.GetExpiredProductsAsync();

            foreach(var product in expiredProducts)
            {
                await _eventBus.Publish(new ProductExpiredEvent(product.Name, product.ExpirationDate!.Value));
            }

            return new NotifyExpiredProductsResponse(expiredProducts.Count);
        }
    }
}