using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using UserManagementApp.ExceptionModule;
using UserManagementApp.Models;

namespace UserManagementApp.RequestManageModule
{
    public class ApiRequestManager
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        HttpClient HttpClient;
        StringContent DataToSend;
        internal HttpResponseMessage Response;
        string EndPoint;
        internal string Result;

        public ApiRequestManager() { }
        public ApiRequestManager(string endpoint, [Optional]object dataToSend,string id=null)
        {
            try
            {
                Log.Info("In try of Constructor of ApiRequestManager");
                HttpClient = new HttpClient();
                string Token = GetToken();
                ThreadContext.Properties["Token"] = Token;
                HttpClient.DefaultRequestHeaders.Add("Token",Token);
                DataToSend = new StringContent(JsonConvert.SerializeObject(dataToSend), Encoding.UTF8, "application/json");
                EndPoint = ConfigurationManager.AppSettings["ServiceUrl"] + @"/" + endpoint;
                SetResult(endpoint, id);
            }
            catch (Exception)
            {
                Log.Info("In catch of constructor of ApiRequestManager");
                throw;
            }
        }

        public void SetResult(string operation,string id)
        {
            try
            {
                Log.Info("In try of SetResult");
                if (operation == "RetrieveAll" || operation == "Create" || operation == "Login")
                    Response = HttpClient.PostAsync(EndPoint, DataToSend).Result;

                else if (operation == "Update")
                    Response = HttpClient.PutAsync(EndPoint, DataToSend).Result;

                else if (operation == "Delete")
                    Response = HttpClient.DeleteAsync(EndPoint + @"/" + id).Result;

                else if (operation == "Retrieve")
                {
                    EndPoint = (id == null ? EndPoint + @"/0" : EndPoint + @"/" + id);
                    Response = HttpClient.GetAsync(EndPoint).Result;
                }

                else if (operation == "Logout")
                    Response = HttpClient.GetAsync(EndPoint).Result;

                Result = Response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception)
            {
                Log.Info("In catch if SetResult");
                throw;
            }
        }

        public List<UserModel> GetResponseData()
        {
            List<UserModel> UserList = new List<UserModel>();
            try
            {
                Log.Info("In try of GetResponseData");
                if (Response.IsSuccessStatusCode)
                    UserList = JsonConvert.DeserializeObject<List<UserModel>>(Result);
                else
                    ApiExceptionManager.ThrowError(Response,Result);
            }
            catch (Exception)
            {
                Log.Info("In catch of GetResponseData");
                throw;
            }
            return UserList;
        }

        public string GetResponseMessage()
        {
            string Message = null;
            try
            {
                Log.Info("In try of GetResponseMessage");
                if (Response.IsSuccessStatusCode)
                    Message = Result;
                else
                    ApiExceptionManager.ThrowError(Response,Result);
            }
            catch (Exception)
            {
                Log.Info("In catch of GetResponseMessage");
                throw;
            }
            return Message;
        }

        public string GetToken()
        {
            string Token = null;
            try
            {
                Log.Info("In try of GetToken");
                Token= HttpContext.Current.Request.Cookies["UserManagementCookie"].Value;
                Token = JsonConvert.DeserializeObject<string>(Token);
            }
            catch (NullReferenceException)
            {
                Log.Info("In catch of GetToken NullrefrenceException");
                Token = null;
            }
            catch
            {
                Log.Info("In catch of GetToken");
                throw;
            }
            return Token;
        }
    }
    
}