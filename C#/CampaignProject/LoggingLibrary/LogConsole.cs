using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggingLibrary
{
    internal class LogConsole : ILoggerUtilities
    {
        public void Init()
        {

        }

        public void LogEvent(string msg)
        {
            Console.WriteLine("LogEvent:" + msg);
        }

        public void LogException(string msg, Exception exception)
        {
            Console.WriteLine("LogError:" + msg + "Exception:" + exception.Message);
        }

        public void LogCheckHoseKeeping()
        {

        }

        public void LogError(string msg)
        {
            Console.WriteLine("LogError:"+msg);
        }
    }
}

