using System;
using System.Collections.Generic;
using UserManagementApi.BLModule.AuthorizationModule;
using UserManagementApi.BLModule.CrudModule;
using log4net;
using System.Reflection;
using UserManagementApi.DataObject;

namespace UserManagementApi
{
    public class UserService : IUserService
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        AuthorizationService AuthorizationService = new AuthorizationService();

        public string Login(LoginData loginDetails)
        {
            string Token = null;
            try
            {
                Log.Info("In try of Login endPoint");
                Token = AuthorizationService.AuthenticateUser(loginDetails);
            }
            catch (Exception e)
            {
                Log.Info("In catch of Login endPoint");
                GlobalException.ThrowError(e);
            }
            return Token;
        }

        [Authorize]
        public List<UserData> RetrieveAll(SortFilterData searchOptions)
        {
            List<UserData> UserList = new List<UserData>();
            try
            {
                Log.Info("In try of RetrieveAll endPoint");
                CrudManager CrudManager = new CrudManager();
                UserList = CrudManager.RetrieveAll(searchOptions);
            }
            catch (Exception e)
            {
                Log.Info("In catch of RetrieveAll endPoint");
                GlobalException.ThrowError(e);
            }

            return UserList;
        }

        [Authorize]
        public string Create(UserData employee)
        {
            try
            {
                Log.Info("In try of Create endPoint");
                CrudManager CrudManager = new CrudManager();
                CrudManager.Create(employee);
            }
            catch (Exception e)
            {
                Log.Info("In catch of Create endPoint");
                GlobalException.ThrowError(e);
            }
            return "Success";

        }

        public List<UserData> Retrieve(string id)
        {
            List<UserData> User = new List<UserData>();
            try
            {
                CrudManager CrudManager = new CrudManager();
                User = CrudManager.Retrieve(id);
            }
            catch(Exception e)
            {
                GlobalException.ThrowError(e);
            }
            return User;
        }

        [Authorize]
        public string Delete(string id)
        {
            try
            {
                Log.Info("In try of Delete endPoint");
                CrudManager CrudManager = new CrudManager();
                CrudManager.Delete(id);
            }
            catch (Exception e)
            {
                Log.Info("In catch of Delete endPoint");
                GlobalException.ThrowError(e);
            }
            return "Success";
        }

        public string Update(UserData employee)
        {
            try
            {
                Log.Info("In try of Update endpoint");
                CrudManager CrudManager = new CrudManager();
                CrudManager.Update(employee);
            }
            catch (Exception e)
            {
                Log.Info("In Catch of Update endPoint");
                GlobalException.ThrowError(e);
            }
            return "Success";
        }

        public string LogOut()
        {
            try
            {
                Log.Info("In try of LogOut endpoint");
                AuthorizationService.LogOut();
            }
            catch (Exception e)
            {
                Log.Info("In Catch of LogOut endPoint");
                GlobalException.ThrowError(e);
            }
            return "User Logged Out";
        }
    }
}
