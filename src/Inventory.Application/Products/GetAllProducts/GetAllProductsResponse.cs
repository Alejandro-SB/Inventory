using Inventory.Application.Products.Dto;
using Inventory.Domain.Entities;
using Inventory.Domain.UseCases;
using System.Collections.Generic;

namespace Inventory.Application.Products.GetAllProducts
{
    public class GetAllProductsResponse : BaseResponse
    {
        public IEnumerable<ProductDto> Products { get; }

        public GetAllProductsResponse(IEnumerable<ProductDto> products)
        {
            Products = products;
        }
    }
}