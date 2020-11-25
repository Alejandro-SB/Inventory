using Inventory.Domain.Entities;
using Inventory.Domain.UseCases;
using System.Collections.Generic;

namespace Inventory.Application.Products.GetAllProducts
{
    public class GetAllProductsResponse : BaseResponse
    {
        public IEnumerable<Product> Products { get; }

        public GetAllProductsResponse(IEnumerable<Product> products)
        {
            Products = products;
        }
    }
}