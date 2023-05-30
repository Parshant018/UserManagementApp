using System;

namespace UserManagementApp.ExceptionModule
{
    public class InvalidDataException:Exception
    {
        public InvalidDataException(string message) : base(message) { }
        public InvalidDataException():base("Invalid Input") { }
    }
}