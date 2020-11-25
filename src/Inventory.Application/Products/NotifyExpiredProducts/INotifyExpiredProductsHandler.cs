using Inventory.Domain.UseCases;

namespace Inventory.Application.Products.NotifyExpiredProducts
{
    public interface INotifyExpiredProductsHandler : IUseCaseHandler<NotifyExpiredProductsRequest, NotifyExpiredProductsResponse>
    {
    }
}