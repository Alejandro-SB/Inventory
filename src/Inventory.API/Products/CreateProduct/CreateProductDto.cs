using System;

namespace Inventory.API.Products.CreateProduct
{
    public class CreateProductDto
    {
        public string? Name { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string? ProductType { get; set; }
    }
}