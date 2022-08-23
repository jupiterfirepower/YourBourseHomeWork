using System;

namespace YB.Todo.Exceptions
{
    public abstract class PlatformWebException : PlatformException
    {
        public abstract int StatusCode { get; }

        public PlatformWebException()
            : base("Web error occurs.")
        {
        }

        public PlatformWebException(string message)
            : base(message)
        {
        }

        public PlatformWebException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
