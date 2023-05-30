using log4net;
using System;
using System.Reflection;
using System.Text.RegularExpressions;
using UserManagementApi.ExceptionModule;

namespace UserManagementApi.BLModule.ValidationModule
{
    public class DateValidator
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static void CheckDateOfBirth(string dateOfBirth)
        {
            try
            {
                Log.Info("In try of CheckDateOfBirth");

                if (dateOfBirth == null)
                    throw new UserInvalidDataException("Please enter date of birth");

                if (!Regex.IsMatch(dateOfBirth, @"^\d{4}-(0[1-9]|1[0-2])-(0[1-9]|1[0-9]|2[0-9]|3[01])$") || Convert.ToDateTime(dateOfBirth) >= DateTime.Now || Convert.ToDateTime(dateOfBirth) == DateTime.MinValue)
                    throw new UserInvalidDataException("Invalid date of birth entered");
            }
            catch (Exception)
            {
                Log.Info("In catch of CheckDateOfBirth");
                throw;
            }
        }
    }
}