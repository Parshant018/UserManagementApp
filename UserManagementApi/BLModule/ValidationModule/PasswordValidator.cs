using log4net;
using System;
using System.Reflection;
using System.Text.RegularExpressions;
using UserManagementApi.ExceptionModule;

namespace UserManagementApi.BLModule.ValidationModule
{
    public class PasswordValidator
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static void CheckPassword(string password)
        {
            try
            {
                Log.Info("In try of CheckPassword");
                if (password == null)
                    throw new UserInvalidDataException("Please enter a password");

                if (!Regex.IsMatch(password, @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$") || password.Length > 30)
                    throw new UserInvalidDataException("Invalid password entered");
            }
            catch (Exception)
            {
                Log.Info("In catch of CheckPassword");
                throw;
            }
        }
    }
}