using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace YB.Todo.Results
{
    /// <summary>
    /// Server Error object result to return from filter.
    /// </summary>
    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(object error)
            : base(error)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}
