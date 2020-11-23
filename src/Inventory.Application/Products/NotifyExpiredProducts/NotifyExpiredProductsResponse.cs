using Inventory.Domain.UseCases;
using System;
using System.Collections.Generic;
using System.Text;

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