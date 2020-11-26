using System;
using System.Runtime.Serialization;

namespace Inventory.Domain.Exceptions
{
    /// <summary>
    /// The exception that is throw when an element is not found
    /// </summary>
    [Serializable]
    public abstract class NotFoundException : ApplicationException
    {
        protected NotFoundException(string? message) : base(message)
        {
        }

        protected NotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            :base(serializationInfo, streamingContext)
        {

        }
    }
}