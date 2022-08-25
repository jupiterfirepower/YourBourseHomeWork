using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YB.Todo.Exceptions;

namespace YB.Todo.Filters
{
    public class GlobalValidateModelActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                throw new PlatformValidationException("Binding or default validation failed.", 
                          context.ModelState.Values.SelectMany(m => m.Errors).Select(m => m.ErrorMessage));
            }

            await next();
        }
    }
}
