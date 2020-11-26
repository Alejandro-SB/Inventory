using System;

namespace Inventory.API.Products.CreateProduct
{
    /// <summary>
    /// Model for creating a product
    /// </summary>
    public class CreateProductDto
    {
        /// <summary>
        /// The name of the product
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// The expiration date of the product
        /// </summary>
        public DateTime? ExpirationDate { get; set; }
        /// <summary>
        /// The type of the product
        /// </summary>
        public string? ProductType { get; set; }
    }
}