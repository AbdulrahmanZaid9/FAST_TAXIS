using System;
using System.IO;
using System.Windows.Forms;

namespace FAST_TAXIS3.Helpers
{
    public static class Logger
    {
        private static readonly string logDirectory = Application.StartupPath + "\\Logs\\";
        private static readonly string logFile = "error_log_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
        private static readonly string logPath = logDirectory + logFile;

        public static void LogError(Exception ex, string additionalInfo = "")
        {
            try
            {
                if (!Directory.Exists(logDirectory))
                    Directory.CreateDirectory(logDirectory);

                using (StreamWriter sw = new StreamWriter(logPath, true))
                {
                    sw.WriteLine("========================================");
                    sw.WriteLine("Date: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    sw.WriteLine("Error: " + ex.Message);
                    sw.WriteLine("Source: " + ex.Source);
                    sw.WriteLine("Stack Trace: " + ex.StackTrace);

                    if (!string.IsNullOrEmpty(additionalInfo))
                        sw.WriteLine("Additional Info: " + additionalInfo);

                    if (ex.InnerException != null)
                        sw.WriteLine("Inner Exception: " + ex.InnerException.Message);

                    sw.WriteLine("========================================");
                    sw.WriteLine();
                }
            }
            catch
            {
                // Silently fail - cannot log error
            }
        }

        public static void LogInfo(string message)
        {
            try
            {
                if (!Directory.Exists(logDirectory))
                    Directory.CreateDirectory(logDirectory);

                using (StreamWriter sw = new StreamWriter(logPath, true))
                {
                    sw.WriteLine("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] INFO: " + message);
                }
            }
            catch
            {
                // Silently fail
            }
        }

        public static void LogWarning(string message)
        {
            try
            {
                if (!Directory.Exists(logDirectory))
                    Directory.CreateDirectory(logDirectory);

                using (StreamWriter sw = new StreamWriter(logPath, true))
                {
                    sw.WriteLine("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] WARNING: " + message);
                }
            }
            catch
            {
                // Silently fail
            }
        }
    }
}