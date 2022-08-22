using System.Collections.Generic;

namespace YB.Todo.Models
{
    public record WebErrorResult
    {
        public IEnumerable<WebError> Errors { get; set; }

        public WebErrorResult() { }

        public WebErrorResult(int code, string header, string message)
        {
            Errors = new List<WebError>()
            {
                new WebError()
                {
                    ErrorCode = code,
                    Header = header,
                    Message = message
                }
            };
        }
    }
}
