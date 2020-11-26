using Inventory.Application.Products.Dto;
using Inventory.Domain.Entities;
using Inventory.Domain.UseCases;

namespace Inventory.Application.Products.RemoveByName
{
    public class RemoveProductByNameResponse : BaseResponse
    {
        public ProductDto Product { get; }

        public RemoveProductByNameResponse(ProductDto product)
        {
            Product = product;
        }
    }
}