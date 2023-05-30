using log4net;
using System;
using System.Net;
using System.Net.Http;
using System.Reflection;

namespace UserManagementApp.ExceptionModule
{
    public class ApiExceptionManager
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static void ThrowError(HttpResponseMessage response, string message)
        {
            try
            {
                Log.Info("In try of ThrowError");
                if (response.StatusCode == HttpStatusCode.BadRequest)
                    throw new InvalidDataException(message);
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                    throw new UserUnAuthorizedException(message);
                else
                    throw new Exception(message);
            }
            catch (Exception)
            {
                Log.Info("In try of throwError");
                throw;
            }
        }
    }
}