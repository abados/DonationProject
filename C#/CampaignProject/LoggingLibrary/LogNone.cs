using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggingLibrary
{
    internal class LogNone : ILoggerUtilities
    {
        public void Init()
        {

        }

        public void LogEvent(string msg)
        {

        }

        public void LogException(string msg, Exception exception)
        {

        }

        public void LogCheckHoseKeeping()
        {

        }

        public void LogError(string msg)
        {

        }
    }
}
