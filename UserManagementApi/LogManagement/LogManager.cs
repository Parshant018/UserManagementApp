using System;
using log4net;
using System.Runtime.CompilerServices;

namespace UserServices
{
    public class LogManager
    {
        public static ILog GetLogger([CallerFilePath]string filePath = "")
        {
            ILog Logger;
            try
            {
                Logger = log4net.LogManager.GetLogger(filePath);
            }
            catch (Exception)
            {
                throw;
            }
            return Logger;
        }
    }
}