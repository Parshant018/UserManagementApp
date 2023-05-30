using System;
using UserManagementApi.ExceptionModule;
using UserManagementApi.DataObjects;
using log4net;
using System.Reflection;
using UserManagementApi.DLModule;
using UserManagementApi.DataObject;
using System.Collections;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace UserManagementApi.BLModule.AuthorizationModule
{
    public class TokenManager
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private string EncryptionKey = "pqrs-aws-mno-aes";
        string Token = HttpContext.Current.Request.Headers["Token"];

        public string GenerateToken(LoginData loginData)
        {
            string Token = null;
            try
            {
                Log.Info("In try of GenerateToken");
                string DataToEncrypt = JsonConvert.SerializeObject(loginData);
                byte[] NormalData = UTF8Encoding.UTF8.GetBytes(DataToEncrypt);

                TripleDESCryptoServiceProvider CryptoService = new TripleDESCryptoServiceProvider();
                CryptoService.Key = UTF8Encoding.UTF8.GetBytes(EncryptionKey);
                CryptoService.Mode = CipherMode.ECB;
                CryptoService.Padding = PaddingMode.PKCS7;

                ICryptoTransform Encryptor = CryptoService.CreateEncryptor();
                byte[] EncryptedData = Encryptor.TransformFinalBlock(NormalData, 0, NormalData.Length);

                CryptoService.Clear();
                Token = Convert.ToBase64String(EncryptedData,0,EncryptedData.Length);
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

                ConnectionManager ConnectionManager = new ConnectionManager(SpName.GetToken,parameterList:new Hashtable{ {"@Token",token } });
                TokenData TokenData = ConnectionManager.GetTokenData();
                TokenData.TokenExpireTime = DateTime.Now.AddHours(4);

                Hashtable TokenParameterList = new Hashtable() { { "@Token",token}, { "@TokenExpireTime",DateTime.UtcNow.AddHours(4)} };

                if (TokenData.Token != null)
                {
                    ConnectionManager TokenUpdateHelper = new ConnectionManager(spName: SpName.UpdateToken, parameterList:TokenParameterList);
                    TokenUpdateHelper.ExecuteNonReadQuery();
                    return;
                }

                TokenData.Token = token;

                ConnectionManager TokenCreateHelper = new ConnectionManager(spName: SpName.CreateToken, parameterList: TokenParameterList);
                TokenCreateHelper.ExecuteNonReadQuery();
            }
            catch (Exception)
            {
                Log.Info("In catch of UpdateTokenData");
                throw;
            }
        }

        public LoginData DecryptToken()
        {
            LoginData LoginData = new LoginData();
            try
            {
                Log.Info("In try of DecryptToken");
                byte[] EncrytedData = Convert.FromBase64String(Token);

                TripleDESCryptoServiceProvider CryptoService = new TripleDESCryptoServiceProvider();
                CryptoService.Key = UTF8Encoding.UTF8.GetBytes(EncryptionKey);
                CryptoService.Mode = CipherMode.ECB;
                CryptoService.Padding = PaddingMode.PKCS7;

                ICryptoTransform Decryptor = CryptoService.CreateDecryptor();
                byte[] DecryptedData = Decryptor.TransformFinalBlock(EncrytedData,0,EncrytedData.Length);
                CryptoService.Clear();

                LoginData = JsonConvert.DeserializeObject<LoginData>(UTF8Encoding.UTF8.GetString(DecryptedData));
            }
            catch (Exception)
            {
                Log.Info("In catch of DecryptToken");
                throw;
            }
            return LoginData;
        }

        public void CheckTokenExpire()
        {
            try
            {
                Log.Info("In try of CheckTokenExpire");
                ConnectionManager ConnectionManager = new ConnectionManager(SpName.GetToken, parameterList:new Hashtable { { "@Token",Token} });
                TokenData TokenData = ConnectionManager.GetTokenData();

                if (TokenData.Token != null)
                    if (TokenData.Token == Token && TokenData.TokenExpireTime >= DateTime.UtcNow)
                        return;

                throw new UserUnAuthorizedException("Session Expired");
            }
            catch (Exception)
            {
                Log.Info("In catch of CheckTokenExpire");
                throw;
            }
        }
    }
}