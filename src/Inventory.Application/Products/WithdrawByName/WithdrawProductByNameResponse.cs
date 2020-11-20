using Inventory.Domain.Entities;
using Inventory.Domain.UseCases;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Products.WithdrawByName
{
    public class WithdrawProductByNameResponse : BaseResponse
    {
        public Product Product { get; }

        public WithdrawProductByNameResponse(Product product)
        {
            Product = product;
        }
    }
}