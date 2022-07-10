using System;
using System.Collections.Generic;
using System.Web.Mvc;
using UserManager.Models;

namespace UserManager.Controllers
{
    public class AdminHomeController : Controller
    {
        [HttpGet]
        public ActionResult Index(SortFilterModel searchOptions)
        {
            List<EmployeeModel> EmployeeList = new List<EmployeeModel>();
            try
            {
                if (searchOptions.FilterOptions != null)
                {
                    searchOptions.FilterOptions.Name = (Request["Name"] != "") ? Request["Name"] : null;
                    searchOptions.FilterOptions.Email = (Request["Email"] != "") ? Request["Email"] : null;
                    searchOptions.FilterOptions.DateOfBirth = (Request["Dob"] != "") ? Request["Dob"] : null;
                    searchOptions.FilterOptions.Designation = (Request["Designation"] != "") ? Request["Designation"] : null;
                    searchOptions.FilterOptions.Age = Convert.ToInt32((Request["Age"] != "") ? Request["Age"] : "0");
                    searchOptions.FilterOptions.PhoneNumber = (Request["phone"] != "") ? Request["PhoneNumber"] : null;
                    searchOptions.FilterOptions.Salary = Convert.ToInt32((Request["Salary"] != "") ? Request["Salary"] : "0");
                }
                ApiRequestManager ApiRequestManager = new ApiRequestManager();
                EmployeeList = ApiRequestManager.GetResponseData<List<EmployeeModel>>(endPoint: "RetrieveAll", searchOptions: searchOptions);
            }
            catch (Exception)
            {
                throw;
            }
            return View(EmployeeList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

       [HttpPost]
       public ActionResult Create(EmployeeModel employee)
       {
            try
            {
                ApiRequestManager ApiRequestManager = new ApiRequestManager();
                string Result = ApiRequestManager.GetResponseData<string>(endPoint: "Create", employee: employee);
            }
            catch (Exception)
            {
                throw;
            }
           return RedirectToAction("Index");
       }

       [HttpGet]
       public ActionResult Update(EmployeeModel employee)
       {
           return View(employee);
       }

        [HttpPost]
        public ActionResult UpdateUser(EmployeeModel employee)
        {
            try
            {
                ApiRequestManager ApiRequestManager = new ApiRequestManager();
                string Result = ApiRequestManager.GetResponseData<string>(endPoint: "Update", employee: employee);
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction("Index");
        }


         [HttpGet]
          public ActionResult RetrieveById(string id)
          {
            List<EmployeeModel> EmployeeList = new List<EmployeeModel>();
            try
            {
                ApiRequestManager ApiRequestManager = new ApiRequestManager();
                EmployeeList = ApiRequestManager.GetResponseData<List<EmployeeModel>>(endPoint: "Retrieve", id: id);
            }
            catch (Exception)
            {
                throw;
            }
              return View("Index",EmployeeList);
          }


       [HttpGet]
       public ActionResult Delete(string id)
       {
            try
            {
                ApiRequestManager ApiRequestManager = new ApiRequestManager();
                string Result = ApiRequestManager.GetResponseData<string>(endPoint: "Delete", id: id);
            }
            catch (Exception)
            {
                throw;
            }
           return RedirectToAction("Index");
       }
    }
}