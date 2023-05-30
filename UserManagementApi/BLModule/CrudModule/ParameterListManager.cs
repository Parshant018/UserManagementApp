using System;
using System.Collections;
using UserManagementApi.DataObject;

namespace UserManagementApi.BLModule.CrudModule
{
    public class ParameterListManager
    {
        public Hashtable GetSearchParameterList(SortFilterData searchOption)
        {

            try
            {
                return new Hashtable() {
                { "@SortOrder",searchOption.SortOrder },
                { "@SortDirection", searchOption.SortDirection },
                { "@SearchText", searchOption.SearchText },
                { "@PageSize", searchOption.PageSize },
                { "@PageIndex", searchOption.PageIndex },
                { "@Name", searchOption.FilterOptions.Name },
                { "@Email", searchOption.FilterOptions.Email },
                { "@DateOfBirth", searchOption.FilterOptions.DateOfBirth },
                { "@Designation", searchOption.FilterOptions.Designation == DesignationList.NULL ? null:searchOption.FilterOptions.Designation.ToString() },
                { "@Salary", searchOption.FilterOptions.Salary },
                { "@PhoneNumber", searchOption.FilterOptions.PhoneNumber }
        };
            }
            catch (Exception)
            {
                throw;
            }
            return null;
        }

        public Hashtable GetUserParameterList(UserData user)
        {

            try
            {
                return new Hashtable() {
                { "@Id",user.Id },
                { "@Name", user.Name },
                { "@Email", user.Email },
                { "@Password", user.Password },
                { "@DateOfBirth", user.DateOfBirth },
                { "@Designation", user.Designation == DesignationList.NULL ? null:user.Designation.ToString() },
                { "@Salary",user.Salary },
                { "@PhoneNumber", user.PhoneNumber },
                { "@Bio", user.Bio }
        };
            }
            catch (Exception)
            {
                throw;
            }
            return null;
        }
    }
}