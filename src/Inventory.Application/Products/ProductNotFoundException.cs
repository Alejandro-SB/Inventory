using Inventory.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Products
{
    public class ProductNotFoundException : NotFoundException
    {
        public string ProductName { get; }

        public ProductNotFoundException(string productName) : base($"Product with name {productName} was not found")
        {
            ProductName = productName;
        }
    }
}