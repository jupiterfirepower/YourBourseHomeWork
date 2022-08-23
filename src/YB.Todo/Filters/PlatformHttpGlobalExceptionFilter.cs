using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using YB.Todo.Exceptions;
using System.Linq;
using YB.Todo.Results;

namespace YB.Todo.Filters
{
    /// <summary>
    /// Global exception filter. Handles Validation exceptions and general Server Errors.
    /// </summary>
    public class PlatformHttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<PlatformHttpGlobalExceptionFilter> _logger;

        public PlatformHttpGlobalExceptionFilter(IWebHostEnvironment env, ILogger<PlatformHttpGlobalExceptionFilter> logger)
        {
            _env = env;
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (!HandlePlatformException(context, context.Exception)
                && (context.Exception.InnerException is null || !HandlePlatformException(context, context.Exception.InnerException)))
            {
                _logger.LogError(context.Exception, context.Exception.Message);

                // For dev and staging, DeveloperExceptionPageMiddleware will handle the error.
                context.Result = new InternalServerErrorObjectResult(new { Message = context.Exception.Message });
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }
        }

        private bool HandlePlatformException(ExceptionContext context, Exception exception)
        {
            if (exception.GetType() == typeof(PlatformValidationException))
            {
                var platformValidationException = (PlatformValidationException)exception;

                var problemDetails = new ValidationProblemDetails()
                {
                    Instance = context.HttpContext.Request.Path,
                    Status = platformValidationException.StatusCode,
                    Detail = "Validation exception, please check the errors for additional details.",
                };

                // To handle binding errors or anything custom.
                if (platformValidationException.ValidationErrors != null && platformValidationException.ValidationErrors.Any())
                {
                    problemDetails.Errors.Add("Validations", platformValidationException.ValidationErrors.ToArray());
                }

                context.Result = new ObjectResult(problemDetails);
                context.HttpContext.Response.StatusCode = platformValidationException.StatusCode;
                return true;
            }
            else if (context.Exception is PlatformWebException platformException)
            {
                context.Result = new ObjectResult(platformException.Message);
                context.HttpContext.Response.StatusCode = platformException.StatusCode;
                return true;
            }

            return false;
        }
    }
}
