using Inventory.API.ActionResults;
using Inventory.Application.Products.CreateProduct;
using Inventory.Domain.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.API.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger, IWebHostEnvironment env)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _env = env ?? throw new ArgumentNullException(nameof(env));
        }

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