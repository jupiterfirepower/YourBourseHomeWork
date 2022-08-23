using System;

namespace YB.Todo.Exceptions
{
    public class PlatformException : Exception
    {
        public PlatformException()
            : base("Platform error occurs.")
        {
        }

        public PlatformException(string message)
            : base(message)
        {
        }

        public PlatformException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
