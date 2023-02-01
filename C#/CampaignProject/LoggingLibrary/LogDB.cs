using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggingLibrary
{
    internal class LogDB : ILoggerUtilities
    {
        //public static string connectionString = System.IO.File.ReadAllText("connectionStr.txt").Replace("\\\\", "\\").Trim();

        public static string connectionString = Environment.GetEnvironmentVariable("ConnectionString");
        public void Init()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    while(Logger.LogItemsQueue.Count > 0)
                    {
                        LogItem item = Logger.LogItemsQueue.Dequeue();
                        if(item != null) { 
                        Insert_RowInDB(item.Message);
                        }

                        System.Threading.Thread.Sleep(1000);
                    }
                    System.Threading.Thread.Sleep(11000);
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
            string Query = "insert into Logs values('"+msg+"','','',GETDATE())";
            LogItem log = new LogItem();
            log.Message = Query;
            log.DateTime = DateTime.Now;
            Logger.LogItemsQueue.Enqueue(log);
        }

        public void LogException(string msg, Exception exception)
        {
            string Query = "insert into Logs values('','"+msg+ "','"+ exception.Message+ "',GETDATE())";
            LogItem log = new LogItem();
            log.Message = Query;
            log.DateTime = DateTime.Now;
            log.Exception = exception;
            Logger.LogItemsQueue.Enqueue(log);
        }

        public void LogCheckHoseKeeping()
        {
            string Query = "DELETE FROM Logs WHERE LogDate < DATEADD(month, -3, GETDATE());";
            Insert_RowInDB(Query);
        }

        public void LogError(string msg)
        {
            string Query = "insert into Logs values('','" + msg + "','',GETDATE())";
            LogItem log = new LogItem();
            log.Message = Query;
            log.DateTime = DateTime.Now;
            Logger.LogItemsQueue.Enqueue(log);
        }

        public void Insert_RowInDB(string updateQuery)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a new SQL command
                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        //Logger.LogEvent("update/delete/insert DB: " + updateQuery, LoggingLibrary.LogLevel.Event);
                        //Execute the command
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
