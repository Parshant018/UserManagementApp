using log4net;
using System;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using UserManagementApp.Models;
using UserManagementApp.RequestManageModule;

namespace UserManagementApp.Controllers
{
    public class LoginController : Controller
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel credentials)
        {
            try
            {
                Log.Info("In try of Login");
                ApiRequestManager ApiRequestManager = new ApiRequestManager(endpoint:"Login", dataToSend:credentials);
                HttpCookie Cookie = new HttpCookie("UserManagementCookie");
                Cookie.Value = ApiRequestManager.GetResponseMessage();
                Cookie.Expires = DateTime.Now.AddHours(4);
                Response.Cookies.Add(Cookie);
                return Json("Success",JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                Log.Info("In catch of Login");
                throw;
            }
        }

        public ActionResult LogOut()
        {
            try
            {
                Log.Info("In try of Logout");
                ApiRequestManager ApiRequestManager = new ApiRequestManager(endpoint:"Logout");
                string Result = ApiRequestManager.GetResponseMessage();
                Response.Cookies["UserManagementCookie"].Expires = DateTime.Now.AddMinutes(-1); 
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                Log.Info("In catch of LogOut");
                throw;
            }
        }
    }
}