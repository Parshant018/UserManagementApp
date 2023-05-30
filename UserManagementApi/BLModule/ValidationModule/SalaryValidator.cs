using log4net;
using System;
using System.Reflection;
using UserManagementApi.ExceptionModule;

namespace UserManagementApi.BLModule.ValidationModule
{
    public class SalaryValidator
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static void CheckSalary(int salary)
        {
            try
            {
                Log.Info("In try of CheckSalary");
                if (salary == 0)
                    throw new UserInvalidDataException("Please enter a salary");
                if (salary < 0 || salary > 10000000)
                    throw new UserInvalidDataException("Invalid salary entered");
            }
            catch (Exception)
            {
                Log.Info("In catch of CheckSalary");
                throw;
            }
        }
    }
}