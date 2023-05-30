using System;
using System.Net;
using System.ServiceModel.Web;
using UserManagementApi.ExceptionModule;
using log4net;
using System.Reflection;
using MySql.Data.MySqlClient;

namespace UserManagementApi
{
    public class GlobalException
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static void ThrowError(Exception exception)
        {
            Log.Info("In Global Exception");
            HttpStatusCode StatusCode;
            string Message = exception.Message;

            if (exception is UserInvalidDataException)
                StatusCode = HttpStatusCode.BadRequest;

            else if (exception is UserUnAuthorizedException)
                StatusCode = HttpStatusCode.Unauthorized;

            else if (exception is MySqlException && exception.Message.Contains("Email"))
                throw new WebFaultException<string>("This email already exists in the system",HttpStatusCode.BadRequest);

            else if (exception is MySqlException && exception.Message.Contains("PhoneNumber"))
                throw new WebFaultException<string>("This phone number already exists in the system", HttpStatusCode.BadRequest);

            else
                throw new WebFaultException<string>("Something went wrong", HttpStatusCode.InternalServerError);

            throw new WebFaultException<string>(exception.Message, StatusCode);
        }
    }
}