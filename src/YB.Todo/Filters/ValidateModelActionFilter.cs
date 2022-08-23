using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using YB.Todo.Exceptions;

namespace YB.Todo.Filters
{
    public class ValidateModelActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = new List<string>();
                foreach (var modelState in context.ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }

                if (errors.Any())
                {
                    throw new PlatformValidationException("Binding or default validation failed.", errors);
                }
            }
        }
    }
}
