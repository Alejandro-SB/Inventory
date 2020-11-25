using Inventory.Domain.Exceptions;
using System;
using System.Runtime.Serialization;

namespace Inventory.Application.Products
{
    [Serializable]
    public class ProductNotFoundException : NotFoundException
    {
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