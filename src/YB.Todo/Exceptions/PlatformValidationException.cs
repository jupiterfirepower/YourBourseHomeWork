using System.Collections.Generic;
using System;
using System.Net;
using System.Linq;

namespace YB.Todo.Exceptions
{
    public class PlatformValidationException : PlatformWebException
    {
        public override int StatusCode => (int)HttpStatusCode.Conflict;

        public IEnumerable<string> ValidationErrors { get; set; }

        public PlatformValidationException()
        {
        }

        public PlatformValidationException(string message)
            : base(message)
        {
        }

        public PlatformValidationException(string message, IEnumerable<string> validationErrors)
            : base(message)
        {
            ValidationErrors = validationErrors;
        }

        public PlatformValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
