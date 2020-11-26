using Inventory.API.Infrastructure.ActionResults;
using Inventory.Application.Products.CreateProduct;
using Inventory.Domain.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace Inventory.API.Infrastructure.Filters
{
    /// <summary>
    /// Filter that captures all exceptions to return the appropriate status code
    /// </summary>
    public class GlobalExceptionFilter : IExceptionFilter
    {
        /// <summary>
        /// The environment configuration
        /// </summary>
        private readonly IWebHostEnvironment _env;
        /// <summary>
        /// Logger class
        /// </summary>
        private readonly ILogger<GlobalExceptionFilter> _logger;

        /// <summary>
        /// Creates an instance of the GlobalExceptionFilter class
        /// </summary>
        /// <param name="logger">Logging class</param>
        /// <param name="env">The environment configuration</param>
        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger, IWebHostEnvironment env)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _env = env ?? throw new ArgumentNullException(nameof(env));
        }

        /// <summary>
        /// Handles exceptions
        /// </summary>
        /// <param name="context">The context of the exception</param>
        public void OnException(ExceptionContext context)
        {
            Exception exception = context.Exception;
            _logger.LogError(new EventId(exception.HResult), exception, exception.Message);

            if(exception is ProductAlreadyExistsException)
            {
                var details = new ValidationProblemDetails
                {
                    Instance = context.HttpContext.Request.Path,
                    Status = StatusCodes.Status422UnprocessableEntity,
                    Detail = "The product already exists"
                };

                details.Errors.Add("Validations", new[] { exception.Message.ToString() });

                context.Result = new UnprocessableEntityObjectResult(details);
                context.HttpContext.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
            }
            else if(exception is NotFoundException notFoundException)
            {
                var result = new NotFoundObjectResult(notFoundException.Message);

                context.Result = result;
                context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            }
            else
            {
                var json = new JsonErrorResponse("An error ocurred");

                if(_env.IsDevelopment())
                {
                    json.DeveloperMessage = exception;
                }

                context.Result = new InternalServerErrorObjectResult(exception);
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }

            context.ExceptionHandled = true;
        }

        private class JsonErrorResponse
        {
            public string[] Messages { get; set; }
            public object? DeveloperMessage { get; set; }

            public JsonErrorResponse(params string[] messages)
            {
                Messages = messages;
            }
        }
    }
}