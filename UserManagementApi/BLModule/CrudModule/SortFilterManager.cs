using System;
using System.Collections.Generic;
using log4net;

namespace UserServices
{
    public class SortFilterManager
    {
        private static readonly ILog Log = LogManager.GetLogger();
        public List<UserInfo> FilterData(SortFilterModel searchOptions)
        {
            List<UserInfo> UserList = new List<UserInfo>();
            try
            {
                Log.Info("In try of FiterData");
                if (searchOptions == null)
                    return CrudManager.GetUserData();
                if (searchOptions.FilterOptions == null)
                    searchOptions.FilterOptions = new SearchFilterModel();
                ConnectionManager<SortFilterModel> ConnectionManager = new ConnectionManager<SortFilterModel>(spName:SpName.RetrieveAllCmd,parameters:searchOptions);
                UserList = ConnectionManager.GetUserList();
            }
            catch (Exception)
            {
                Log.Info("In catch of FilterData");
                throw;
            }
            return UserList;
        }
    }
}