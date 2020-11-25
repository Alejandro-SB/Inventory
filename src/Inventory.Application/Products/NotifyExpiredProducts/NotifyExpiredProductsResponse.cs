using Inventory.Domain.UseCases;

namespace Inventory.Application.Products.NotifyExpiredProducts
{
    public class NotifyExpiredProductsResponse : BaseResponse
    {
        public int ExpiredProducts { get; }

        public NotifyExpiredProductsResponse(int expiredProducts)
        {
            ExpiredProducts = expiredProducts;
        }
    }
}