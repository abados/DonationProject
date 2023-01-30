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
        public static string connectionString = System.IO.File.ReadAllText("connectionStr.txt").Replace("\\\\", "\\").Trim();
        public void Init()
        {

        }

        public void LogEvent(string msg)
        {
            string Query = "insert into Logs values('"+msg+"','','',GETDATE())";
            Insert_RowInDB(Query);
        }

        public void LogException(string msg, Exception exception)
        {
            string Query = "insert into Logs values('','"+msg+ "','"+ exception.Message+ "',GETDATE())";
            Insert_RowInDB(Query);
        }

        public void LogCheckHoseKeeping()
        {
            string Query = "DELETE FROM Logs WHERE LogDate < DATEADD(month, -3, GETDATE());";
            Insert_RowInDB(Query);
        }

        public void LogError(string msg)
        {
            string Query = "insert into Logs values('','" + msg + "','',GETDATE())";
            Insert_RowInDB(Query);
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
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
