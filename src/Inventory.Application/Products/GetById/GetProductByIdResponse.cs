using Inventory.Domain.Entities;
using Inventory.Domain.UseCases;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Products.GetById
{
    public class GetProductByIdResponse : BaseResponse
    {
        public Product? Product { get; set; }

        public GetProductByIdResponse(Product? product)
        {
            Product = product;
        }
    }
}