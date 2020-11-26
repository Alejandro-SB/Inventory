using System;
using System.Runtime.Serialization;

namespace Inventory.Web.Api
{
    /// <summary>
    /// Exception that is throw when a request to the API has been rejected for bad authentication
    /// </summary>
    [Serializable]
    public class ApiAuthenticationException : ApplicationException
    {
        public ApiAuthenticationException()
        {

        }

        protected ApiAuthenticationException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {

        }
    }
}