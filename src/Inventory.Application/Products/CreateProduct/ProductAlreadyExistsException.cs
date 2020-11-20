using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Products.CreateProduct
{
    /// <summary>
    /// Exception produced when creating a product that already exists
    /// </summary>
    public sealed class ProductAlreadyExistsException : ApplicationException
    {
        /// <summary>
        /// The name of the product that was being created
        /// </summary>
        public string ProductName { get; }

        /// <summary>
        /// Initializes a new instance of the ProductAlreadyExistsException
        /// </summary>
        /// <param name="productName">The name of the product being created</param>
        internal ProductAlreadyExistsException(string productName) : base($"Product {productName} already exists")
        {
            ProductName = productName;
        }
    }
}