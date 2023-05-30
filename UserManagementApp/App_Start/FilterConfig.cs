using System.Web.Mvc;
using UserManagementApp.ExceptionModule;

namespace UserManagementApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new GlobalExceptionHandler());
        }
    }
}
