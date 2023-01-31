using LoggingLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CampaignProject.DAL
{
    
    public class SqlQuery
    {
        
        //public static string connectionString = System.IO.File.ReadAllText("connectionStr.txt").Replace("\\\\", "\\").Trim();

        public static string connectionString = Environment.GetEnvironmentVariable("ConnectionString");


        public delegate object SetDataReader_delegate(SqlDataReader reader);

        public static object getDataFromDB(string SqlQuery, SetDataReader_delegate Ptrfunc)
        {
           
        object retHash = null;
           
            using (
                SqlConnection connection = new SqlConnection(connectionString))
            {

                // Adapter
                using (SqlCommand command = new SqlCommand(SqlQuery, connection))
                {

                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    //Reader
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Logger.LogEvent("Get Data from DB: "+ SqlQuery, LoggingLibrary.LogLevel.Event);
                        retHash = Ptrfunc(reader);

                    }
                }
            }
            return retHash;
        }

        public static object getOneDataFromDB(string SqlQuery)
        {
            object retHash = null;
            
         

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                // Adapter
                using (SqlCommand command = new SqlCommand(SqlQuery, connection))
                {
                    connection.Open();
                    //Reader
                   Logger.LogEvent("Get 1 Data from DB: " + SqlQuery, LoggingLibrary.LogLevel.Event);
                    retHash = command.ExecuteScalar().ToString();
                    

                    
                }
            }
            return retHash;
        }

       public static string getOneDataFromDBInString(string SqlQuery)
        {


            string id;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                // Adapter
                using (SqlCommand command = new SqlCommand(SqlQuery, connection))
                {
                    connection.Open();
                    //Reader
                    Logger.LogEvent("Get 1 Data from DB: " + SqlQuery, LoggingLibrary.LogLevel.Event);
                    id = command.ExecuteScalar().ToString();



                }
            }
            return id;
        }
 
        public static void Update_Delete_Insert_RowInDB(string updateQuery)
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
                        Logger.LogEvent("update/delete/insert DB: " + updateQuery, LoggingLibrary.LogLevel.Event);
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

        public static object insertIntoConnectedTable(string SqlQuery)
        {
            object retHash = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                // Adapter
                using (SqlCommand command = new SqlCommand(SqlQuery, connection))
                {
                   Logger.LogEvent("Insert into Users: " + SqlQuery, LoggingLibrary.LogLevel.Event);
                    connection.Open();
                    //Reader

                    retHash = command.ExecuteScalar();



                }
            }
            return retHash;
        }


    }
}