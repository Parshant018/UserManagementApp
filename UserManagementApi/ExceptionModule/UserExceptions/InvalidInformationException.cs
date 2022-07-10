using System;
namespace UserServices.ExceptionModule
{
    public class InvalidInformationException:Exception
    {
        public InvalidInformationException() : base("Inavlid Information Entered") { }
        public InvalidInformationException(string message) : base(message) { }
    }
}