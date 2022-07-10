using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using UserManager.Models;

namespace UserManager.Controllers
{
    public class ApiRequestManager
    {
        HttpClient HttpClient;
        public ApiRequestManager()
        {
            try
            {
                HttpClient = new HttpClient();
                HttpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["ServiceUrl"]);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public HttpResponseMessage GetResponse(string operation,string endPoint,[Optional]StringContent dataToPass,string id=null)
        {
            HttpResponseMessage ResponseMessage = null;
            try
            {
                //string RequestType = HttpContext.Current.Request.HttpMethod;
                if (operation == "Retrieve")
                    ResponseMessage = HttpClient.GetAsync(endPoint + "/" + id).Result;
                else if (operation == "Create" || operation == "RetrieveAll")
                    ResponseMessage = HttpClient.PostAsync(endPoint, dataToPass).Result;
                else if (operation == "Update")
                    ResponseMessage = HttpClient.PutAsync(endPoint, dataToPass).Result;
                else if (operation == "Delete")
                    ResponseMessage = HttpClient.DeleteAsync(endPoint + "/" + id).Result;
            }
            catch (Exception)
            {
                throw;
            }
            return ResponseMessage;
        }

        public T GetResponseData<T>(string endPoint,SortFilterModel searchOptions =null,EmployeeModel employee = null,string id = null)
        {
            List<EmployeeModel> EmployeeList = new List<EmployeeModel>();
            try
            {
                StringContent DataToPass = null;
                if (searchOptions != null)
                    DataToPass = GetStringContent(searchOptions);
                if (employee != null)
                    DataToPass = GetStringContent(employee);
                HttpResponseMessage Response = GetResponse(endPoint, HttpClient.BaseAddress + @"/" + endPoint, DataToPass, id);
                string Result = Response.Content.ReadAsStringAsync().Result;
                if (Response.IsSuccessStatusCode)
                {
                    if (typeof(T).ToString() == "System.String")
                        return (T)Convert.ChangeType(Result, typeof(T));
                    EmployeeList = JsonConvert.DeserializeObject<List<EmployeeModel>>(Result);
                }
                else
                {
                    throw new Exception(Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return (T)Convert.ChangeType(EmployeeList,typeof(T));
        }


        public StringContent GetStringContent<T>(T dataToPass)
        {
            StringContent Data = null;
            try
            {
                Data = new StringContent(JsonConvert.SerializeObject(dataToPass), Encoding.UTF8, "application/json");
            }
            catch (Exception)
            {
                throw;
            }
            return Data;
        }
    }
}