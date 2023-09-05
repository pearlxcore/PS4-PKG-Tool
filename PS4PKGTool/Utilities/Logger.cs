using Microsoft.Extensions.Logging;
using PS4PKGTool.Utilities.PS4PKGToolHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS4PKGTool.Utilities
{
    class Logger
    {
        public static string LogFilename { get; set; }
        public static void FlushLog()
        {
            File.WriteAllText(Helper.PS4PKGToolLogFile, string.Empty);
        }

        private static object lockObject = new object(); // For thread safety

        public static void LogInformation(string msg)
        {
            Log(LogLevel.Information, msg);
        }

        public static void LogWarning(string msg)
        {
            Log(LogLevel.Warning, msg);
        }

        public static void LogError(string msg, Exception ex = null)
        {
            Log(LogLevel.Error, msg, ex);
        }

        private static void Log(LogLevel level, string msg, Exception ex = null)
        {
            try
            {
                if (!string.IsNullOrEmpty(msg))
                {
                    lock (lockObject) // For thread safety
                    {
                        using (var sw = new StreamWriter(Helper.PS4PKGToolLogFile, true))
                        {
                            DateTime now = DateTime.Now;
                            string logMessage = $"{now:G} : [{level.ToString().Replace("Information", "INFO").Replace("Warning","WARN").Replace("Error","ERR")}] {msg}";

                            if (ex != null)
                            {
                                logMessage += Environment.NewLine + ex.ToString();
                            }

                            sw.WriteLine(logMessage);
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Handle any exceptions during logging
            }
        }
    }
}
