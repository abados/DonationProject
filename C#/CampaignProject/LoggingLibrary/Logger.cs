using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggingLibrary
{
    public enum LogLevel
    {
        Event = 1,
        Warning = 2,
        Error = 3
    }
    public static class Logger
    {
        public static void Log(string message, LogLevel level)
        {
            // Code to write the log message to a file or database, or send it to a logging service.
        }
    }
}
