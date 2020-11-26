using Inventory.Application.Products.Dto;
using Inventory.Domain.Entities;
using Inventory.Domain.UseCases;

namespace Inventory.Application.Products.GetByName
{
    public class GetProductByNameResponse : BaseResponse
    {
        public ProductDto? Product { get; set; }

        public GetProductByNameResponse(ProductDto? product)
        {
            Product = product;
        }
    }
}