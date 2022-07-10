using System;
using System.Collections.Generic;
using UserServices.BLModule;
using UserServices.ExceptionModule;
using log4net;

namespace UserServices
{
    public class CrudManager
    {
        private static readonly ILog Log = LogManager.GetLogger();
        InfoValidator InfoValidator = new InfoValidator();
        public static List<UserInfo> GetUserData()
        {
            List<UserInfo> UserList = new List<UserInfo>();
            try
            {
                Log.Info("In try of GetData");
                ConnectionManager<UserInfo> ConnectionManager = new ConnectionManager<UserInfo>(spName:SpName.AllEmployeesCmd);
                UserList = ConnectionManager.GetUserList();
            }
            catch(Exception)
            {
                Log.Info("In catch of GetData");
                throw;
            }
            return UserList;
        }

        public void CreateUser(UserInfo user)
        {
            try
            {
                Log.Info("In try of CreateUser");
                InfoValidator.ValidateInfo(user);
                List<UserInfo> UserList = RetrieveById(user.Id.ToString());
                if (UserList.Count > 0)
                    throw new InvalidInformationException("User With this Id Already Exists");
                ConnectionManager<UserInfo> ConnectionManager = new ConnectionManager<UserInfo>(spName:SpName.CreateCmd,parameters:user);
                ConnectionManager.ExecuteNonReadQuery();
            }
            catch (Exception)
            {
                Log.Info("in catch of Create User");
                throw;
            }
        }

        public void DeleteUser(string id)
        {
            try
            {
                Log.Info("In try of DeleteUser");
                ConnectionManager<UserInfo> ConnectionManager = new ConnectionManager<UserInfo>(spName:SpName.DeleteCmd,id:id);
                ConnectionManager.ExecuteNonReadQuery();
            }
            catch (Exception)
            {
                Log.Info("In catch of DeleteUser");
                throw;
            }
        }

        public List<UserInfo> RetrieveById(string id)
        {
            List<UserInfo> EmployeeList = new List<UserInfo>();
            try
            {
                Log.Info("In try of RetrieveById");
                ConnectionManager<UserInfo> ConnectionManager = new ConnectionManager<UserInfo>(spName:SpName.RetrieveCmd,id:id);
                EmployeeList = ConnectionManager.GetUserList();
            }
            catch (Exception)
            {
                Log.Info("In catch of RetrieveById");
                throw;
            }
            return EmployeeList;
        }

        public void UpdateUser(UserInfo user)
        {
            try
            {
                Log.Info("In try of UpdateUser");
                InfoValidator.ValidateInfo(user);
                ConnectionManager<UserInfo> ConnectionManager = new ConnectionManager<UserInfo>(spName:SpName.UpdateCmd,parameters:user);
                ConnectionManager.ExecuteNonReadQuery();
            }
            catch (Exception)
            {
                Log.Info("In catch of UpdateUser");
                throw;
            }
        }
    }
}