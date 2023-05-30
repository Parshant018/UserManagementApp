using System;
using System.Collections.Generic;
using UserManagementApi.BLModule.ValidationModule;
using log4net;
using System.Reflection;
using UserManagementApi.DataObject;
using UserManagementApi.DLModule;
using System.Collections;
using UserManagementApi.BLModule.AuthorizationModule;
using UserManagementApi.ExceptionModule;

namespace UserManagementApi.BLModule.CrudModule
{
    public class CrudManager
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        ValidationHelper ValidationHelper = new ValidationHelper();
        ParameterListManager ParameterListManager = new ParameterListManager();

        public List<UserData> RetrieveAll(SortFilterData searchOption)
        {
            List<UserData> UserList = new List<UserData>();
            ParameterListManager ParameterListManager = new ParameterListManager();
            try
            {
                Log.Info("In try of FiterData");
                if (searchOption == null)
                    searchOption = new SortFilterData();

                if (searchOption.FilterOptions == null)
                    searchOption.FilterOptions = new SearchFilterData();

                ConnectionManager ConnectionManager = new ConnectionManager(spName: SpName.RetrieveAll, parameterList: ParameterListManager.GetSearchParameterList(searchOption));
                UserList = ConnectionManager.GetUserList();

            }
            catch (Exception)
            {
                Log.Info("In catch of FilterData");
                throw;
            }
            return UserList;
        }

        public void Create(UserData user)
        {
            try
            {
                Log.Info("In try of CreateUser");

                Guid Id = Guid.NewGuid();
                user.Id = Id.ToString();

                ValidationHelper.ValidateUserData(user);

                ConnectionManager ConnectionManager = new ConnectionManager(spName: SpName.CreateUser, parameterList: ParameterListManager.GetUserParameterList(user));
                ConnectionManager.ExecuteNonReadQuery();

            }
            catch (Exception)
            {
                Log.Info("in catch of Create User");
                throw;
            }
        }

        public void Delete(string id)
        {
            try
            {
                Log.Info("In try of DeleteUser");
                ConnectionManager ConnectionManager = new ConnectionManager(spName: SpName.DeleteUser, parameterList: new Hashtable() { { "@id",id } });
                ConnectionManager.ExecuteNonReadQuery();
            }
            catch (Exception)
            {
                Log.Info("In catch of DeleteUser");
                throw;
            }
        }

        public List<UserData> Retrieve(string id)
        {
            List<UserData> User = new List<UserData>();
            TokenManager TokenManager = new TokenManager();
            try
            {
                Log.Info("In try of RetrieveUserById");
                LoginData LoginData = TokenManager.DecryptToken();
                TokenManager.CheckTokenExpire();

                if (LoginData.Designation == DesignationList.User)
                    id = LoginData.Id;

                ConnectionManager ConnectionManager = new ConnectionManager(spName: SpName.RetrieveUser, parameterList: new Hashtable() { { "@id", id }});
                User = ConnectionManager.GetUserList();
            }
            catch (Exception)
            {
                Log.Info("In catch of RetrieveUserById");
                throw;
            }
            return User;
        }

        public void Update(UserData user)
        {
            try
            {
                Log.Info("In try of UpdateUser");
                ValidationHelper.ValidateUserData(user);

                List<UserData> OldUserData = new List<UserData>();
                LoginData LoginData = new LoginData();
                TokenManager TokenManager = new TokenManager();

                LoginData = TokenManager.DecryptToken();
                OldUserData = Retrieve(user.Id.ToString());

                if ((user.Id != LoginData.Id && (LoginData.Designation==DesignationList.User || !user.Password.Equals(OldUserData[0].Password))) || (LoginData.Designation == DesignationList.User && user.Designation == DesignationList.Admin))
                    throw new UserUnAuthorizedException("User UnAuthorized");

                ConnectionManager ConnectionManager = new ConnectionManager(spName: SpName.UpdateUser, parameterList: ParameterListManager.GetUserParameterList(user));
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