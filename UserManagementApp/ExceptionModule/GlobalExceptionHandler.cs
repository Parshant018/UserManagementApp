using System.Net;
using System.Web.Mvc;

namespace UserManagementApp.ExceptionModule
{
    public class GlobalExceptionHandler : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            if (filterContext.Exception is InvalidDataException)
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            if (filterContext.Exception is UserUnAuthorizedException)
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            else
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            filterContext.Result = new JsonResult()
            {
                Data = filterContext.Exception.Message,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}