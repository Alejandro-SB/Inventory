using Inventory.Domain.Entities;
using Inventory.Domain.UseCases;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Products.GetByName
{
    public class GetProductByNameResponse : BaseResponse
    {
        public Product? Product { get; set; }

        public GetProductByNameResponse(Product? product)
        {
            Product = product;
        }
    }
}