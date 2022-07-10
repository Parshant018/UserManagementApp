using System;

namespace UserServices.ExceptionModule
{
    public class UnAuthorizedAccessException:Exception
    {
        public UnAuthorizedAccessException() : base("User UnAuthorized") { }
        public UnAuthorizedAccessException(string message) : base(message) { }
    }
}