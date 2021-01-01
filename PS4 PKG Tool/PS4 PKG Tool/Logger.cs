using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS4_PKG_Tool
{
    class Logger
    {
        /**
         * Log file path
         *  
         * @var string
         */

        private static string status_;
        private static string LogFilename_;
        public static string LogFilename
        {
            get
            {
                return LogFilename_;
            }
            set
            {
                LogFilename_ = value;
            }
        }

        private static string __log_file_path;
        static string logFile;
        /**
         * __log_file_path get/set
         */
        public static string filePath
        {
            get { return Logger.__log_file_path; }
            set { if (value.Length > 0) Logger.__log_file_path = value; }
        }

        /**
         * Flush log file contents
         *  
         * @return void
         */
        public static void flush()
        {
            File.WriteAllText(Logger.filePath, string.Empty);
        }

        /**
         * Log message
         *  
         * @param string msg
         * @return void
         */
        public static void log(string msg)
        {
           

            Logger.filePath = Environment.CurrentDirectory + @"\PS4_PKG_Tool_LogFile.txt";

            if (msg.Length > 0)
            {
                using (var sw = new StreamWriter(Logger.filePath, true))
                {
                    DateTime d = DateTime.Now;
                    sw.WriteLine("{0:G} : {1}", d, msg);
                    sw.Flush();
                    sw.Close();
                }
            }
        }


    }
}
