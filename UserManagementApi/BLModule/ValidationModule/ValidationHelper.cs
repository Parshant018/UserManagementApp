using System;
using System.Collections.Generic;
using log4net;
using System.Reflection;
using UserManagementApi.DataObject;

namespace UserManagementApi.BLModule.ValidationModule
{
    public class ValidationHelper
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public void ValidateUserData(UserData dataToCheck)
        {
            List<string> DesignatoinList = new List<string>() { "Admin", "User" };
            try
            {
                Log.Info("In try of ValidateUserData");

                NameValidator.CheckName(dataToCheck.Name);

                DateValidator.CheckDateOfBirth(dataToCheck.DateOfBirth);

                EmailValidator.CheckEmail(dataToCheck.Email);

                PasswordValidator.CheckPassword(dataToCheck.Password);

                PhoneNumberValidator.CheckPhoneNumber(dataToCheck.PhoneNumber);

                SalaryValidator.CheckSalary(dataToCheck.Salary);
            }
            catch (Exception)
            {
                Log.Info("In catch of ValidateUserData");
                throw;
            }
        }


        public void ValidateLoginCredentials(LoginData loginCredentials)
        {
            EmailValidator.CheckEmail(loginCredentials.Email);

            PasswordValidator.CheckPassword(loginCredentials.Password);
        }
    }
}