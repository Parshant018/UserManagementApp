using System;
namespace UserManagementApi.ExceptionModule
{
    public class UserInvalidDataException:Exception
    {
        public UserInvalidDataException() : base("Invalid Information Entered") { }
        public UserInvalidDataException(string message) : base(message) { }
    }
}