using Inventory.Application.Products.Dto;
using Inventory.Domain.Entities;
using System;

namespace Inventory.API.Products.GetProduct
{
    /// <summary>
    /// Class that represents the view model of the product
    /// </summary>
    public class ProductViewModel
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
        /// Creates an instance of the ProductViewModel class
        /// </summary>
        /// <param name="product">The product to map from</param>
        public ProductViewModel(ProductDto product)
        {
            Name = product.Name;
            ExpirationDate = product.ExpirationDate;
            ProductType = product.ProductType;
        }
    }
}