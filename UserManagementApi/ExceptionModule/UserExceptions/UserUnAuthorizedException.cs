using System;

namespace UserManagementApi.ExceptionModule
{
    public class UserUnAuthorizedException:Exception
    {
        public UserUnAuthorizedException() : base("User UnAuthorized") { }
        public UserUnAuthorizedException(string message) : base(message) { }
    }
}