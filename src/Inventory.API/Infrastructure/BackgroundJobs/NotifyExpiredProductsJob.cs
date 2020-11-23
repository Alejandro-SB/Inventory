using Hangfire;
using Hangfire.Common;
using Inventory.Application.Products.NotifyExpiredProducts;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.API.Infrastructure.BackgroundJobs
{
    public class NotifyExpiredProductsJob
    {
        private readonly INotifyExpiredProductsHandler _handler;

        public NotifyExpiredProductsJob(INotifyExpiredProductsHandler handler)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        public Task Run()
        {
            return _handler.Handle(new NotifyExpiredProductsRequest());
        }
    }
}