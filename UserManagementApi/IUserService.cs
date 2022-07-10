using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using UserServices.Models;

namespace UserServices
{
    [ServiceContract]
    public interface IUserService
    {
        [WebInvoke(Method = "POST" , UriTemplate = "/RetrieveAll" , RequestFormat = WebMessageFormat.Json , ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        List<UserInfo> RetrieveAll(SortFilterModel searchOptions);

        [WebInvoke(Method = "POST", UriTemplate = "/Login", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        string Login(LoginInfo loginDetails);

        [WebInvoke(Method ="POST" , UriTemplate = "/Create" , RequestFormat = WebMessageFormat.Json , ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        string Create(UserInfo employee);

        [WebInvoke(Method = "DELETE" , UriTemplate = "/Delete/{id}" , RequestFormat = WebMessageFormat.Json , ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        string Delete(string id);

        [WebInvoke(Method = "GET" , UriTemplate = "/Retrieve/{id}" , RequestFormat = WebMessageFormat.Json , ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        List<UserInfo> Retrieve(string id);

        [WebInvoke(Method = "PUT" , UriTemplate = "/Update" , RequestFormat = WebMessageFormat.Json , ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        string Update(UserInfo employee);
    }
}
