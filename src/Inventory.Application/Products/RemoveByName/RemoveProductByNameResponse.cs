using Inventory.Domain.Entities;
using Inventory.Domain.UseCases;

namespace Inventory.Application.Products.RemoveByName
{
    public class RemoveProductByNameResponse : BaseResponse
    {
        public Product Product { get; }

        public RemoveProductByNameResponse(Product product)
        {
            Product = product;
        }
    }
}