using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using UserManagementApi.DataObject;

namespace UserManagementApi
{
    [ServiceContract]
    public interface IUserService
    {
        [WebInvoke(Method = "POST" , UriTemplate = "/RetrieveAll" , RequestFormat = WebMessageFormat.Json , ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        List<UserData> RetrieveAll(SortFilterData searchOptions);

        [WebInvoke(Method = "POST", UriTemplate = "/Login", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        string Login(LoginData loginDetails);

        [WebInvoke(Method ="POST" , UriTemplate = "/Create" , RequestFormat = WebMessageFormat.Json , ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        string Create(UserData employee);

        [WebInvoke(Method = "DELETE" , UriTemplate = "/Delete/{id}" , RequestFormat = WebMessageFormat.Json , ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        string Delete(string id);

        [WebInvoke(Method = "GET" , UriTemplate = "/Retrieve/{id}" , RequestFormat = WebMessageFormat.Json , ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        List<UserData> Retrieve(string id);

        [WebInvoke(Method = "PUT" , UriTemplate = "/Update" , RequestFormat = WebMessageFormat.Json , ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        string Update(UserData employee);

        [WebInvoke(Method = "GET", UriTemplate = "/LogOut", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string LogOut();
    }
}
