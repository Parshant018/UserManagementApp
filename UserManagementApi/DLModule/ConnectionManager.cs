using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Runtime.InteropServices;
using UserServices.Models;
using log4net;
using UserServices.ExceptionModule;

namespace UserServices
{
    public class ConnectionManager<T>
    {
        private static readonly ILog Log = LogManager.GetLogger();
        ParameterManager ParameterManager = new ParameterManager();
        MySqlConnection Connection = new MySqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
        MySqlCommand Command;
        public ConnectionManager(string spName,[Optional]T parameters,string id = null,string token=null)
        {
            try
            {
                Log.Info("In try of ConnectionManager Constructor");
                Command = new MySqlCommand(spName, Connection);
                Command.CommandType = CommandType.StoredProcedure;
                ParameterManager.AddParameters(Command, parameters, id,token);
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
                    throw new InvalidInformationException("No user Found with this Id");
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

        public List<UserInfo> GetUserList()
        {
            List<UserInfo> UserList = new List<UserInfo>();
            try
            {
                Log.Info("In try of GetUserList");
                using (MySqlDataReader Reader = Command.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        UserInfo User = new UserInfo();
                        User.Id = Convert.ToInt32(Reader["Id"]);
                        User.Name = Convert.ToString(Reader["Name"]);
                        User.Email = Convert.ToString(Reader["Email"]);
                        User.Password = Convert.ToString(Reader["Password"]);
                        User.DateOfBirth = Convert.ToDateTime(Reader["DateOfBirth"]).ToString("yyyy-MM-dd");
                        User.Designation = Convert.ToString(Reader["Designation"]);
                        User.Age = Convert.ToInt32(Reader["Age"]);
                        User.Salary = Convert.ToInt32(Reader["Salary"]);
                        User.PhoneNumber = Convert.ToString(Reader["PhoneNumber"]);
                        User.Bio = Convert.ToString(Reader["Bio"]);
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


        public TokenInfo GetTokenInfo()
        {
            TokenInfo TokenInfo = new TokenInfo();
            try
            {
                Log.Info("In try of GetTokenInfo");
                using (MySqlDataReader Reader = Command.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        TokenInfo.Token = Convert.ToString(Reader["Token"]);
                        TokenInfo.TokenExpireTime = Convert.ToDateTime(Reader["ExpireTime"]);
                    }
                }
            }
            catch (Exception)
            {
                Log.Info("In catch of GetTokenInfo");
                throw;
            }
            finally
            {
                Log.Info("In finally of GetTokenInfo");
                Connection.Close();
            }
            return TokenInfo;
        }
    }
}