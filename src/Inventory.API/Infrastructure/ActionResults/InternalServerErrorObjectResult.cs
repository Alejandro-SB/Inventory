using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.API.Infrastructure.ActionResults
{
    /// <summary>
    /// Class that represents the result of a 500 status code error
    /// </summary>
    class InternalServerErrorObjectResult : ObjectResult
    {
        /// <summary>
        /// Initializes an instance of the InternalServerErrorObjectResult class
        /// </summary>
        /// <param name="error">The error produced</param>
        public InternalServerErrorObjectResult(object error) : base(error)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}