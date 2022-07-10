using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UserServices.ExceptionModule;
using log4net;

namespace UserServices.BLModule
{
    public class InfoValidator
    {
        private static readonly ILog Log = LogManager.GetLogger();
        public void ValidateInfo(UserInfo dataToCheck)
        {
            List<string> DesignatoinList = new List<string>() { "Admin", "User"};
            try
            {
                Log.Info("In try of ValidateInfo");
                if (dataToCheck.Id <= 0 || dataToCheck.Id >= 20000)
                    throw new InvalidInformationException("Invalid Id Entered");
                if (dataToCheck.Name == null || dataToCheck.Name.Length > 50 || !Regex.IsMatch(dataToCheck.Name, @"^[a-zA-Z]+ [a-zA-Z]+$"))
                    throw new InvalidInformationException("Invalid Name Entered");
                if (dataToCheck.Email == null || !Regex.IsMatch(dataToCheck.Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$") || dataToCheck.Email.Length > 50)
                    throw new InvalidInformationException("Invalid Email Entered");
                if (dataToCheck.Password == null || !Regex.IsMatch(dataToCheck.Password, @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$") || dataToCheck.Password.Length > 30)
                {
                    if (dataToCheck.Password.Contains(";"))
                        throw new InvalidInformationException("Password cannot have ;");
                    throw new InvalidInformationException("Invalid Password Entered");
                }
                if (dataToCheck.DateOfBirth == null || !Regex.IsMatch(dataToCheck.DateOfBirth, @"^\d{4}-(0[1-9]|1[0-2])-(0[1-9]|1[0-9]|2[0-9]|3[01])$") || Convert.ToDateTime(dataToCheck.DateOfBirth)>=DateTime.Now)
                    throw new InvalidInformationException("Invalid DateOfBirth Entered");
                if (dataToCheck.Designation == null || !DesignatoinList.Contains(dataToCheck.Designation))
                    throw new InvalidInformationException("Invalid Designation Entered");
                if (dataToCheck.Age <= 0 || dataToCheck.Age>110)
                    throw new InvalidInformationException("Invalid Age Entered");
                if (dataToCheck.Salary <= 0 || dataToCheck.Salary > 10000000)
                    throw new InvalidInformationException("Invalid Salary Entered");
                if (dataToCheck.PhoneNumber == null || !Regex.IsMatch(dataToCheck.PhoneNumber, @"^\d{12}"))
                    throw new InvalidInformationException("Invalid Phone Number Entered");
            }
            catch(Exception)
            {
                Log.Info("In catch of ValidateInfo");
                throw;
            }
        }
    }
}