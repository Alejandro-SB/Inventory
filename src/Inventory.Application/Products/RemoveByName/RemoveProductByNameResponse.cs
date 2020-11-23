using Inventory.Domain.Entities;
using Inventory.Domain.UseCases;
using System;
using System.Collections.Generic;
using System.Text;

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