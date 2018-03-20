namespace Ruiagf.Framework.Utils
{
    using System;

    public class TimesToRetryAfterDeadlockExceededException : Exception
    {
        public TimesToRetryAfterDeadlockExceededException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
