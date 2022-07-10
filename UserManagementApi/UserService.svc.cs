using System;
using System.Collections.Generic;
using UserServices.BLModule.AuthorizationManageModule;
using UserServices.Models;
using log4net;

namespace UserServices
{
    public class UserService : IUserService
    {
        private static readonly ILog Log = LogManager.GetLogger();
        AuthorizationService AuthorizationService = new AuthorizationService();

        public string Login(LoginInfo loginDetails)
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

        public List<UserInfo> RetrieveAll(SortFilterModel searchOptions)
        {
            List<UserInfo> FilteredList = new List<UserInfo>();
            try
            {
                Log.Info("In try of RetrieveAll endPoint");
                AuthorizationService.AuthorizeUser();
                SortFilterManager SortFilterManager = new SortFilterManager();
                FilteredList = SortFilterManager.FilterData(searchOptions);
            }
            catch (Exception e)
            {
                Log.Info("In catch of RetrieveAll endPoint");
                GlobalException.ThrowError(e);
            }

            return FilteredList;
        }

        public string Create(UserInfo employee)
        {
            try
            {
                Log.Info("In try of Create endPoint");
                AuthorizationService.AuthorizeUser();
                CrudManager CrudManager = new CrudManager();
                CrudManager.CreateUser(employee);
            }
            catch (Exception e)
            {
                Log.Info("In catch of Create endPoint");
                GlobalException.ThrowError(e);
            }
            return "User Created";

        }

        public List<UserInfo> Retrieve(string id)
        {
            List<UserInfo> Employee = new List<UserInfo>();
            try
            {
                Log.Info("In try of Retrieve endPoint");
                AuthorizationService.AuthorizeUser();
                CrudManager CrudManager = new CrudManager();
                Employee = CrudManager.RetrieveById(id);
            }
            catch (Exception e)
            {
                Log.Info("In catch of Retrieve endPoint");
                GlobalException.ThrowError(e);
            }
            return Employee;
        }

        public string Delete(string id)
        {
            try
            {
                Log.Info("In try of Delete endPoint");
                AuthorizationService.AuthorizeUser();
                CrudManager CrudManager = new CrudManager();
                CrudManager.DeleteUser(id);
            }
            catch (Exception e)
            {
                Log.Info("In catch of Delete endPoint");
                GlobalException.ThrowError(e);
            }
            return "User Deleted";
        }

        public string Update(UserInfo employee)
        {
            try
            {
                Log.Info("In try of Update endpoint");
                AuthorizationService.AuthorizeUser();
                CrudManager CrudManager = new CrudManager();
                CrudManager.UpdateUser(employee);
            }
            catch (Exception e)
            {
                Log.Info("In Catch of Update endPoint");
                GlobalException.ThrowError(e);
            }
            return "User Updated";
        }
    }
}
