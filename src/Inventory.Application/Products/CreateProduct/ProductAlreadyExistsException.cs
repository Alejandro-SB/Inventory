using System;
using System.Runtime.Serialization;

namespace Inventory.Application.Products.CreateProduct
{
    /// <summary>
    /// Exception produced when creating a product that already exists
    /// </summary>
    [Serializable]
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

        private ProductAlreadyExistsException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            :base(serializationInfo, streamingContext)
        {
            ProductName = string.Empty;
        }
    }
}