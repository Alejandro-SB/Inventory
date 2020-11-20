using Inventory.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Domain.UseCases
{
    /// <summary>
    /// Class that represents the basic response of a use case
    /// </summary>
    public abstract class BaseResponse
    {
        /// <summary>
        /// Indicates whether the procedure was successful. True by default
        /// </summary>
        public bool Success { get; } = true;
        /// <summary>
        /// Contains the message of the response
        /// </summary>
        public string? Message { get; }

        /// <summary>
        /// Initializes a new instance of the base response
        /// </summary>
        protected BaseResponse()
        {
        }

        /// <summary>
        /// Initializes a new instance of a failed response with the error message given
        /// </summary>
        /// <param name="message">The error message</param>
        protected BaseResponse(string message) : this(false, message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the base response indicating wheter or not it succeeded and the message
        /// </summary>
        /// <param name="success"></param>
        /// <param name="message"></param>
        protected BaseResponse(bool success, string message)
        {
            if(message.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(message));
            }

            Success = success;
            Message = message;
        }
    }
}