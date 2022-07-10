using System;
using System.Collections.Generic;
using System.Web;
using UserServices.ExceptionModule;
using UserServices.Models;
using log4net;

namespace UserServices.BLModule.AuthorizationManageModule
{
    public class AuthorizationService
    {
        private static readonly ILog Log = LogManager.GetLogger();
        TokenManager TokenManager = new TokenManager();
        public string AuthenticateUser(LoginInfo loginInfo)
        {
            try
            {
                Log.Info("In try of Authenticate User");
                ConnectionManager<LoginInfo> ConnectionManager = new ConnectionManager<LoginInfo>(spName: SpName.LoginUserCmd, parameters: loginInfo);
                List<UserInfo> UserList = ConnectionManager.GetUserList();
                if (UserList.Count > 0)
                    {
                    string Token = TokenManager.GenerateToken(loginInfo.Email, loginInfo.Password);
                    TokenManager.UpdateTokenData(Token);
                    return Token;
                    }
                throw new InvalidInformationException("User User Found with This Mail Id and Password");
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
                string Token = HttpContext.Current.Request.Headers["Token"];
                if (Token == null)
                    throw new UnAuthorizedAccessException();
                TokenManager.CheckTokenExpire(Token);
                LoginInfo LoginInfo = new LoginInfo();
                LoginInfo = TokenManager.DecryptToken(Token);
                ConnectionManager<LoginInfo> ConnectionManager = new ConnectionManager<LoginInfo>(spName: SpName.LoginUserCmd, parameters: LoginInfo);
                List<UserInfo> UserList = ConnectionManager.GetUserList();
                if (UserList.Count == 0)
                    throw new UnAuthorizedAccessException("Invalid Token Entered");
                UserInfo User = UserList[0];
                if (LoginInfo.Email == User.Email && LoginInfo.Password == User.Password && User.Designation.ToUpper() == "ADMIN")
                {
                  return;
                }
                throw new UnAuthorizedAccessException();
            }
            catch (Exception)
            {
                Log.Info("In catch of AuthorizeUser");
                throw;
            }
        }


    }
}