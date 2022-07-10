using MySql.Data.MySqlClient;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using log4net;

namespace UserServices
{
    public class ParameterManager
    {
        private static readonly ILog Log = LogManager.GetLogger();
        public void AddParameters<T>(MySqlCommand command,[Optional]T parameters,string id=null,string token=null)
        {
            try
            {
                Log.Info("In try of AddParameters");
                if (id != null)
                    command.Parameters.AddWithValue("@userId", Convert.ToInt32(id));
                if (token != null)
                    command.Parameters.AddWithValue("@Token",token);

                if (parameters != null)
                {
                    PropertyInfo[] EmployeeProp = parameters.GetType().GetProperties();
                    foreach (PropertyInfo property in EmployeeProp)
                    {
                        if (property.Name == "FilterOptions")
                            AddParameters(command:command, parameters:(SearchFilterModel)property.GetValue(parameters));
                        else if (property.Name == "SortDirection")
                            command.Parameters.AddWithValue("@SortDirection", (int)property.GetValue(parameters));
                        else
                            command.Parameters.AddWithValue("@" + property.Name, property.GetValue(parameters));
                    }
                }
            }
            catch (Exception)
            {
                Log.Info("In catch of AddParameters");
                throw;
            }
        }
    }
}