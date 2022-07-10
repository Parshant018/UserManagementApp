using System;
using System.Net;
using System.ServiceModel.Web;
using UserServices.ExceptionModule;
using log4net;

namespace UserServices
{
    public class GlobalException
    {
        private static readonly ILog Log = LogManager.GetLogger();
        public static void ThrowError(Exception exception)
        {
            Log.Info("In Global Exception");
            HttpStatusCode StatusCode;
            if (exception is InvalidInformationException)
                StatusCode = HttpStatusCode.BadRequest;
            else if (exception is UnAuthorizedAccessException)
                StatusCode = HttpStatusCode.Unauthorized;
            else
                throw new WebFaultException<string>("Something went wrong", HttpStatusCode.InternalServerError);
            throw new WebFaultException<string>(exception.Message, StatusCode);
        }
    }
}