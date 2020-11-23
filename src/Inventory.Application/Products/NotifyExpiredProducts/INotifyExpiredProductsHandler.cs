using Inventory.Domain.UseCases;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Products.NotifyExpiredProducts
{
    public interface INotifyExpiredProductsHandler : IUseCaseHandler<NotifyExpiredProductsRequest, NotifyExpiredProductsResponse>
    {
    }
}