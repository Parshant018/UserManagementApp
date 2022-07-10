using System;
using System.Web.Mvc;

namespace UserManager.ExceptionModule
{
    public class GlobalExceptionHandler : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            throw new NotImplementedException();
        }
    }
}