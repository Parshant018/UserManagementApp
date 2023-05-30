using log4net;
using System;
using System.Reflection;
using System.Text.RegularExpressions;
using UserManagementApi.ExceptionModule;

namespace UserManagementApi.BLModule.ValidationModule
{
    public class PhoneNumberValidator
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static void CheckPhoneNumber(string phoneNumber)
        {
            try
            {
                Log.Info("In try of CheckPhoneNumber");

                if (phoneNumber == null)
                    throw new UserInvalidDataException("Please enter a phone number");

                if (!Regex.IsMatch(phoneNumber, @"^\d{12}"))
                    throw new UserInvalidDataException("Invalid phone number entered");
            }
            catch (Exception)
            {
                Log.Info("In catch of CheckPhoneNumber");
                throw;
            }
        }
    }
}