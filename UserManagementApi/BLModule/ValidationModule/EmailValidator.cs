using log4net;
using System;
using System.Reflection;
using System.Text.RegularExpressions;
using UserManagementApi.ExceptionModule;

namespace UserManagementApi.BLModule.ValidationModule
{
    public class EmailValidator
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static void CheckEmail(string email)
        {
            try
            {
                Log.Info("In try of CheckEmail");
                if (email == null)
                    throw new UserInvalidDataException("Please enter an email");

                if (!Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$") || email.Length > 50 || email.Contains("<") || email.Contains(">"))
                    throw new UserInvalidDataException("Invalid email entered");
            }
            catch (Exception)
            {
                Log.Info("In catch of CheckEmail");
                throw;
            }
        }
    }
}