using Inventory.Domain.Entities;
using System;

namespace Inventory.API.Products.GetProduct
{
    public class ProductResponse
    {
        public string Name { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string? ProductType { get; set; }

        public ProductResponse(Product product)
        {
            Name = product.Name;
            ExpirationDate = product.ExpirationDate;
            ProductType = product.ProductType;
        }
    }
}