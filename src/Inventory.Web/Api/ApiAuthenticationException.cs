using System;
using System.Runtime.Serialization;

namespace Inventory.Web.Api
{
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