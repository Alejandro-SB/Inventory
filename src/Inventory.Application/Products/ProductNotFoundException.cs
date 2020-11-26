using Inventory.Domain.Exceptions;
using System;
using System.Runtime.Serialization;

namespace Inventory.Application.Products
{
    /// <summary>
    /// The exception that is throw when a product is not found and required for an operation
    /// </summary>
    [Serializable]
    public class ProductNotFoundException : NotFoundException
    {
        /// <summary>
        /// The name of the product
        /// </summary>
        public string ProductName { get; }

        public ProductNotFoundException(string productName) : base($"Product with name {productName} was not found")
        {
            ProductName = productName;
        }

        protected ProductNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
            ProductName = string.Empty;
        }
    }
}