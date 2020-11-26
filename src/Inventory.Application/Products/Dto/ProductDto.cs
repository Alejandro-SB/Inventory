using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Products.Dto
{
    /// <summary>
    /// Class that represents a product
    /// </summary>
    public class ProductDto
    {
        /// <summary>
        /// The name of the product
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The expiration date of the product
        /// </summary>
        public DateTime? ExpirationDate { get; set; }
        /// <summary>
        /// The type of the product
        /// </summary>
        public string? ProductType { get; set; }
        
        /// <summary>
        /// Creates an instance of the ProductDto class
        /// </summary>
        /// <param name="name">The name of the product</param>
        public ProductDto(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Creates an instance of the ProductDto class
        /// </summary>
        /// <param name="product">The product to map from</param>
        internal ProductDto(Product product)
        {
            Name = product.Name;
            ExpirationDate = product.ExpirationDate;
            ProductType = product.ProductType;
        }
    }
}