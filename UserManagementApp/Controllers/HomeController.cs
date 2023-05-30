using System;
using System.Collections.Generic;
using log4net;
using System.Web.Mvc;
using UserManagementApp.Models;
using UserManagementApp.RequestManageModule;
using System.Reflection;

namespace UserManagementApp.Controllers
{
    public class HomeController : Controller
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        ApiRequestManager ApiRequestManager = new ApiRequestManager();

        [HttpGet]
        public ActionResult Index()
        {
            try{
                if (ApiRequestManager.GetToken() == null)
                    return RedirectToAction("Index","Login");
            }
            catch (Exception)
            {
                throw;
            }
            return View();
        }

        [HttpPost]
        public ActionResult SearchData(SortFilterModel searchModel) {
            List<UserModel> UserList = new List<UserModel>();
            try
            {
                Log.Info("In try of SearchData");
                ApiRequestManager ApiRequestManager = new ApiRequestManager(endpoint: "RetrieveAll", dataToSend: searchModel);
                UserList = ApiRequestManager.GetResponseData();
            }
            catch (Exception)
            {
                Log.Info("In catch of SearchData");
                throw;
            }
            return View(UserList);
        }

        [HttpGet]
        public ActionResult CreateUser()
        {
            try
            {
                if (ApiRequestManager.GetToken() == null)
                    return RedirectToAction("Index", "Login");
            }
            catch (Exception)
            {
                throw;
            }
            return View();
        }

       [HttpPost]
       public ActionResult CreateUser(UserModel user)
       {
            string Result = null;
            try
            {
                Log.Info("In try of CreateUser");
                ApiRequestManager ApiRequestManager = new ApiRequestManager(endpoint:"Create",dataToSend:user);
                Result = ApiRequestManager.GetResponseMessage();
            }
            catch (Exception)
            {
                Log.Info("In catch of CreateUser");
                throw;
            }
           return Json(Result,JsonRequestBehavior.AllowGet);
       }

       [HttpGet]
       public ActionResult Update(string id)
       {
            List<UserModel> User = new List<UserModel>();
            try
            {
                if (ApiRequestManager.GetToken() == null)
                    return RedirectToAction("Index", "Login");
                ApiRequestManager ApiRequestManagerObj = new ApiRequestManager(endpoint: "Retrieve", id: id);
                User = ApiRequestManagerObj.GetResponseData();
                if (User.Count == 0)
                    return RedirectToAction("OnError");
            }
            catch (Exception)
            {
                throw;
            }
            return View(User[0]);
       }

        [HttpPost]
        public ActionResult UpdateUser(UserModel user)
        {
            string Result = null;
            try
            {
                Log.Info("In try of UpdateUser");
                ApiRequestManager ApiRequestManager = new ApiRequestManager(endpoint:"Update",dataToSend:user);
                Result = ApiRequestManager.GetResponseMessage();
            }
            catch (Exception)
            {
                Log.Info("In catch of Update User");
                throw;
            }
            return Json(Result,JsonRequestBehavior.AllowGet);
        }


         [HttpGet]
       public ActionResult RetrieveUser(string id = null)
        {
            List<UserModel> UserList = new List<UserModel>();
            try
            {
                Log.Info("In try of RetrieveUser");
                if (ApiRequestManager.GetToken() == null)
                    return RedirectToAction("Index","Login");
                ApiRequestManager ApiRequestManagerObj = new ApiRequestManager(endpoint: "Retrieve",id:id);
                UserList = ApiRequestManagerObj.GetResponseData();
                if (UserList.Count == 0)
                    return RedirectToAction("OnError");
            }
            catch (Exception)
            {
                Log.Info("In catch of RetrieveUser");
                throw;
            }
              return View(UserList[0]);
          }

        [HttpGet]
        public ActionResult OnError()
        {
            try
            {
                if (ApiRequestManager.GetToken() == null)
                    return RedirectToAction("Index", "Login");
            }
            catch (Exception)
            {
                throw;
            }
            return View();
        }


       [HttpGet]
       public ActionResult DeleteUser(string id)
       {
            try
            {
                Log.Info("In try of DeleteUser");
                ApiRequestManager ApiRequestManager = new ApiRequestManager(endpoint:"Delete",id:id);
                string Result = ApiRequestManager.GetResponseMessage();
            }
            catch (Exception)
            {
                Log.Info("In catch of DeleteUser");
                throw;
            }
           return Json("User Deleted",JsonRequestBehavior.AllowGet);
       }
    }
}