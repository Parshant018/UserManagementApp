using System;

namespace UserManagementApp.ExceptionModule
{
    public class UserUnAuthorizedException:Exception
    {
        public UserUnAuthorizedException() : base("Invalid input") { }
        public UserUnAuthorizedException(string message) : base(message) { }
    }
}