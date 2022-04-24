using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace eCommerce.src.ServiceLayer.ResultService
{
    public class Logger
    {
        private static string path = Path.GetFullPath(@"..\..\..\Logs\");
        private static object _myLock = new object();
        private static Logger logger = null;

        private Logger() { }

        public static Logger GetInstance()
        {
            if (logger == null) // The first check
            {
                lock (_myLock)
                {
                    if (logger == null) // The second (double) check
                    {
                        logger = new Logger();
                    }
                }
            }
            return logger;
        }

        public void LogError(string msg)
        {
            string errorLogger = path + "LogError.log";
            try
            {
                if (!File.Exists(errorLogger))
                {
                    File.Create(errorLogger);
                }
                using (StreamWriter sw = File.AppendText(errorLogger))
                {
                    sw.WriteLine(DateTime.Now.ToString() + ": " + msg);
                    sw.Dispose();
                    sw.Close();
                }
            }
            catch
            { }
        }

        public void LogInfo(string msg)
        {
            string infoLogger = path + "LogInfo.log";
            try
            {
                if (!File.Exists(infoLogger))
                {
                    File.Create(infoLogger);
                }
                using (StreamWriter sw = File.AppendText(infoLogger))
                {
                    sw.WriteLine(DateTime.Now.ToString() + ": " + msg);
                    sw.Dispose();
                    sw.Close();
                }
            }
            catch { }
        }
    }
}
