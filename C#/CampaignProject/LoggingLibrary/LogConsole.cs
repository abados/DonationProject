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
            Task.Run(() =>
            {
                while (true)
                {
                    if (Logger.LogItemsQueue.Count > 0)
                    {
                        LogItem item = Logger.LogItemsQueue.Dequeue();
                        if(item != null ) { 
                        Console.WriteLine("Log: "+item.Message+ " Time: "+ item.DateTime);
                        }
                        System.Threading.Thread.Sleep(2000);

                    }

                    System.Threading.Thread.Sleep(1000);
                }

            });

            Task.Run(() =>
            {
                while (true)
                {
                    LogCheckHoseKeeping();

                    System.Threading.Thread.Sleep(180000);
                }

            });
        }

        public void LogEvent(string msg)
        {
            string consoleMsg = "LogEvent:" + msg;
            LogItem log = new LogItem();
            log.Message = consoleMsg;
            log.DateTime = DateTime.Now;
            Logger.LogItemsQueue.Enqueue(log);
        }

        public void LogException(string msg, Exception exception)
        {
            string consoleMsg = "LogError:" + msg + "Exception:" + exception.Message;
            LogItem log = new LogItem();
            log.Message = consoleMsg;
            log.DateTime = DateTime.Now;
            log.Exception = exception;
            Logger.LogItemsQueue.Enqueue(log);
        }

        public void LogCheckHoseKeeping()
        {

        }

        public void LogError(string msg)
        {
           
            string consoleMsg = "LogError:" + msg;
            LogItem log = new LogItem();
            log.Message = consoleMsg;
            log.DateTime = DateTime.Now;
            Logger.LogItemsQueue.Enqueue(log);
        }
    }
}

