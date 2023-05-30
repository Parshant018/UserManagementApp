using System;
using System.Collections.Generic;
using System.Web;
using UserManagementApi.ExceptionModule;
using log4net;
using System.Reflection;
using UserManagementApi.DLModule;
using UserManagementApi.DataObject;
using System.Collections;
using UserManagementApi.BLModule.ValidationModule;
using UserManagementApi.BLModule.CrudModule;

namespace UserManagementApi.BLModule.AuthorizationModule
{
    public class AuthorizationService
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        TokenManager TokenManager = new TokenManager();
        CrudManager CrudManager = new CrudManager();

        public string AuthenticateUser(LoginData loginData)
        {
            try
            {
                Log.Info("In try of Authenticate User");
                ValidationHelper ValidationHelper = new ValidationHelper();
                ValidationHelper.ValidateLoginCredentials(loginData);

                ConnectionManager ConnectionManager = new ConnectionManager(spName: SpName.LoginUser, parameterList: new Hashtable { { "@Email", loginData.Email }, { "@Password",loginData.Password } });
                List<UserData> User = ConnectionManager.GetUserList();

                if (User.Count > 0)
                    {
                    loginData.Designation = User[0].Designation;
                    loginData.Id = User[0].Id;

                    string Token = TokenManager.GenerateToken(loginData);
                    TokenManager.UpdateTokenData(Token);

                    return Token;
                    }

                throw new UserInvalidDataException("No user found with this email or password");
            }
            catch (Exception)
            {
                Log.Info("In catch of Authenticate User");
                throw;
            }
        }

        public void AuthorizeUser()
        {
            try
            {
                Log.Info("In try of AuthorizeUser");
                LoginData LoginData = TokenManager.DecryptToken();
                List<UserData> User =  CrudManager.Retrieve(LoginData.Id);

                ThreadContext.Properties["Id"] = User[0].Email;

                if (User[0].Designation == DesignationList.User)
                    throw new UserUnAuthorizedException("User");
            }
            catch (Exception)
            {
                Log.Info("In catch of AuthorizeUser");
                throw;
            }
        }

        public void LogOut()
        {
            try
            {
                string Token = HttpContext.Current.Request.Headers["Token"];

                if (Token == null)
                    throw new UserUnAuthorizedException("User Not Logged In");

                ConnectionManager ConnectionManager = new ConnectionManager(spName: SpName.DeleteToken, parameterList: new Hashtable { { "@Token", Token } });
                ConnectionManager.ExecuteNonReadQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}