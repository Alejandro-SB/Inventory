using Inventory.Application.Products.NotifyExpiredProducts;
using System;
using System.Threading.Tasks;

namespace Inventory.API.Infrastructure.BackgroundJobs
{
    /// <summary>
    /// Class that notifies all the expired products
    /// </summary>
    public class NotifyExpiredProductsJob
    {
        /// <summary>
        /// The handler to notify all expired products
        /// </summary>
        private readonly INotifyExpiredProductsHandler _handler;

        /// <summary>
        /// Creates an instance of the NotifyExpiredProductsJob class
        /// </summary>
        /// <param name="handler">The handler to notify all the expired products</param>
        public NotifyExpiredProductsJob(INotifyExpiredProductsHandler handler)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        /// <summary>
        /// Executes the handler
        /// </summary>
        /// <returns></returns>
        public Task Run()
        {
            return _handler.Handle(new NotifyExpiredProductsRequest());
        }
    }
}