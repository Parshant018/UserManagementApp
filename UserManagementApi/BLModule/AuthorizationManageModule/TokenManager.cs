using System;
using UserServices.ExceptionModule;
using UserServices.Models;
using log4net;

namespace UserServices.BLModule.AuthorizationManageModule
{
    public class TokenManager
    {
        private static readonly ILog Log = LogManager.GetLogger();
        public string GenerateToken(string email, string password)
        {
            string Token = null;
            try
            {
                Log.Info("In try of GenerateToken");
                foreach (char letter in email)
                {
                    Token += (char)(letter + 1);
                }
                Token += "<";
                foreach (char letter in password)
                {
                    Token += (char)(letter + 1);
                }
            }
            catch (Exception)
            {
                Log.Info("In catch of GenerateToken");
                throw;
            }
            return Token;
        }

        public void UpdateTokenData(string token)
        {
            try
            {
                Log.Info("In try of UpdateTokenData");
                ConnectionManager<TokenInfo> ConnectionManager = new ConnectionManager<TokenInfo>(SpName.GetTokenCmd,token:token);
                TokenInfo TokenInfo = ConnectionManager.GetTokenInfo();
                TokenInfo.TokenExpireTime = DateTime.Now.AddHours(4);
                if (TokenInfo.Token != null)
                {
                    ConnectionManager<TokenInfo> TokenUpdateHelper = new ConnectionManager<TokenInfo>(spName:SpName.UpdateTokenCmd, parameters:TokenInfo);
                    TokenUpdateHelper.ExecuteNonReadQuery();
                    return;
                }
                TokenInfo.Token = token;
                ConnectionManager<TokenInfo> TokenCreateHelper = new ConnectionManager<TokenInfo>(spName: SpName.CreateTokenCmd, parameters: TokenInfo);
                TokenCreateHelper.ExecuteNonReadQuery();
            }
            catch (Exception)
            {
                Log.Info("In catch of UpdateTokenData");
                throw;
            }
        }

        public LoginInfo DecryptToken(string token)
        {
            LoginInfo UserInfo = new LoginInfo();
            try
            {
                Log.Info("In try of DecryptToken");
                string[] UserCredentials = token.Split('<');
                foreach (char letter in UserCredentials[0])
                {
                    UserInfo.Email += (char)(letter - 1);
                }

                foreach (char letter in UserCredentials[1])
                {
                    UserInfo.Password += (char)(letter - 1);
                }
            }
            catch (Exception)
            {
                Log.Info("In catch of DecryptToken");
                throw;
            }
            return UserInfo;
        }

        public void CheckTokenExpire(string token)
        {
            try
            {
                Log.Info("In try of CheckTokenExpire");
                ConnectionManager<TokenInfo> ConnectionManager = new ConnectionManager<TokenInfo>(SpName.GetTokenCmd, token: token);
                TokenInfo TokenInfo = ConnectionManager.GetTokenInfo();
                if (TokenInfo.Token != null)
                    if (TokenInfo.Token == token && TokenInfo.TokenExpireTime >= DateTime.Now)
                        return;
                throw new UnAuthorizedAccessException("Session Expired");
            }
            catch (Exception)
            {
                Log.Info("In catch of CheckTokenExpire");
                throw;
            }
        }
    }
}