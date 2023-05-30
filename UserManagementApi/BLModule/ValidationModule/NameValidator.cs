using System;
using UserManagementApi.ExceptionModule;

namespace UserManagementApi.BLModule.ValidationModule
{
    public class NameValidator
    {
        public static void CheckName(string name)
        {
            try
            {
                if (name == null)
                    throw new UserInvalidDataException("Please enter a name");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}