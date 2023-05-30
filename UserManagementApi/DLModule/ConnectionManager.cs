using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Runtime.InteropServices;
using UserManagementApi.DataObjects;
using log4net;
using UserManagementApi.ExceptionModule;
using System.Reflection;
using UserManagementApi.DataObject;
using System.Collections;

namespace UserManagementApi.DLModule
{
    public class ConnectionManager
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        MySqlConnection Connection = new MySqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
        MySqlCommand Command;

        public ConnectionManager(string spName,[Optional]Hashtable parameterList)
        {
            try
            {
                Log.Info("In try of ConnectionManager Constructor");
                Command = new MySqlCommand(spName, Connection);
                Command.CommandType = CommandType.StoredProcedure;

                if (parameterList != null)
                {
                    foreach (string key in parameterList.Keys)
                    {
                            Command.Parameters.AddWithValue(key, parameterList[key]);
                    }
                }

                Connection.Open();
            }
            catch (Exception)
            {
                Log.Info("In catch of ConnectionManager Constructor");
                throw;
            }
        }

        public void ExecuteNonReadQuery()
        {
            try
            {
                Log.Info("In try of ExecuteNonReadquery");

                int AffectedRowCount = Command.ExecuteNonQuery();

                if (AffectedRowCount == 0)
                    throw new UserInvalidDataException("No user Found with this Id");
            }
            catch (Exception)
            {
                Log.Info("In catch of ExecuteNonReadquery");
                throw;
            }
            finally
            {
                Log.Info("In finally of ExecuteNonReadquery");
                Connection.Close();
            }
        }

        public List<UserData> GetUserList()
        {
            List<UserData> UserList = new List<UserData>();
            try
            {
                Log.Info("In try of GetUserList");
                using (MySqlDataReader Reader = Command.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        UserData User = new UserData();
                        User.Id = Convert.ToString(Reader["Id"]);
                        User.Name = Convert.ToString(Reader["Name"]);
                        User.Email = Convert.ToString(Reader["Email"]);
                        User.Password = Convert.ToString(Reader["Password"]);
                        User.DateOfBirth = Convert.ToDateTime(Reader["DateOfBirth"]).ToString("yyyy-MM-dd");
                        User.Designation = Convert.ToString(Reader["Designation"]).ToUpper() == "ADMIN" ? DesignationList.Admin:DesignationList.User;
                        User.Salary = Convert.ToInt32(Reader["Salary"]);
                        User.PhoneNumber = Convert.ToString(Reader["PhoneNumber"]);
                        User.Bio = Convert.ToString(Reader["Bio"]);
                        User.CreatedOn = Convert.ToDateTime(Reader["CreatedOn"]).ToString("yyyy-MM-dd HH:mm:ss");
                        User.ModifiedOn = Convert.ToDateTime(Reader["ModifiedOn"]).ToString("yyyy-MM-dd HH:mm:ss");
                        UserList.Add(User);
                    }
                }
            }
            catch (Exception)
            {
                Log.Info("In catch of GetUserList");
                throw;
            }
            finally
            {
                Log.Info("In finally of GetUserList");
                Connection.Close();
            }
            return UserList;
        }


        public TokenData GetTokenData()
        {
            TokenData TokenData = new TokenData();
            try
            {
                Log.Info("In try of GetTokenData");
                using (MySqlDataReader Reader = Command.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        TokenData.Token = Convert.ToString(Reader["Token"]);
                        TokenData.TokenExpireTime = Convert.ToDateTime(Reader["ExpireTime"]);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Info("In catch of GetTokenData");
                throw;
            }
            finally
            {
                Log.Info("In finally of GetTokenData");
                Connection.Close();
            }
            return TokenData;
        }
    }
}