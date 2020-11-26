using Inventory.Domain.UseCases;

namespace Inventory.Application.Products.NotifyExpiredProducts
{
    /// <summary>
    /// Use case to manage notification of expired products
    /// </summary>
    public interface INotifyExpiredProductsHandler : IUseCaseHandler<NotifyExpiredProductsRequest, NotifyExpiredProductsResponse>
    {
    }
}