using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LoggingLibrary.LogFile;
using static System.Net.Mime.MediaTypeNames;

namespace LoggingLibrary
{

    public enum LogLevel
    {
        Event = 1,
        Warning = 2,
        Error = 3
    }
    public class LogItem
    {
        public DateTime DateTime { get; set; }
        public Exception Exception { get; set; }
        public string Message { get; set; }
    }
   

    public interface ILoggerUtilities
    {
        void Init();
        void LogEvent(string msg);
        void LogError(string msg);
        void LogException(string msg, Exception exce);
        void LogCheckHoseKeeping();
    }
    public class Logger 
    {
        public Logger(string providerName) {

            if (providerName == "DB")
            {
                myLog = new LogDB();

            } else if (providerName == "File")
            {
                myLog = new LogFile();
            }
            else if(providerName == "Console")
            {
                myLog = new LogConsole();
            }
            else
            {
                myLog = new LogNone();
            }

            myLog.Init();
        }

        static ILoggerUtilities myLog;
        public static System.Collections.Generic.Queue<LogItem> LogItemsQueue;

        public void LogEvent(string message, LogLevel level)
        {
            if (myLog != null)
            {

                myLog.LogEvent("" + message + " " + level);
                
            }
            
        }

        public void LogError(string message, LogLevel level)
        {
            if (myLog != null)
            {
                myLog.LogError("" + message + " " + level);
            }
        }
        public void LogException(string message, Exception exception)
        {
            if (myLog != null)
            {
                myLog.LogException(message, exception);
            }
        }

    }
}
