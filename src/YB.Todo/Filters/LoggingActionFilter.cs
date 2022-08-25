using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace YB.Todo.Filters
{
    public class LoggingActionFilter : IAsyncActionFilter
    {
        private readonly ILogger _logger;

        public LoggingActionFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<LoggingActionFilter>();
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger.LogInformation($"Action '{context.ActionDescriptor.DisplayName}' executing.");

            await next();

            _logger.LogInformation($"Action '{context.ActionDescriptor.DisplayName}' executed.");
        }
    }
}
