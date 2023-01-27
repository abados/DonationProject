using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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
        public static string connectionString = System.IO.File.ReadAllText("connectionStr.txt").Replace("\\\\", "\\").Trim();
        private static string fileName = getConnectionStringFromDB();
        private static string originalPath = fileName;
        //private static string fileName = "C:\\Users\\User\\source\\Projects\\Semester2\\MidProject\\C#\\CampaignProject\\logs\\Log.txt";
        //private static string originalPath = fileName;
        private static string path;
        private static string baseName;
        private static string newFileName;
        private static int fileNumber = 1;
        private static readonly object _lock = new object();
        private static int MAXSIZE = 1024*1024 ; //1 MEGA
        

        public static void Log(string message, LogLevel level)
        {

            Task.Run(() => {

                path = Path.GetDirectoryName(fileName);// get the directory path of the file

                baseName = Path.GetFileNameWithoutExtension(originalPath); //get the base name of the file (without extension or numbers)

                FileInfo file = new FileInfo(fileName);   // create a FileInfo object to check the size of the file

                CheckSize(file);

                // Write the log message to a file
                lock (_lock)
                {

                    string text = $"{DateTime.Now} {level}: {message}";
                    WriteToFile(text);

                }
            });

          
        }

        public static void WriteToFile(string text)  // function to write text to the file
        {
            FileInfo file = new FileInfo(fileName);   // create a FileInfo object to check the size of the file

            CheckSize(file);

            using (StreamWriter sw = File.AppendText(fileName))
            {
                sw.WriteLine(text);
            }
        }

        public static void CheckSize(FileInfo file)  // function to write text to the file
        {

            if (file.Length >= MAXSIZE)   // check if the file size is greater than or equal to 1000kb
            {
                newFileName = path + "\\" + baseName + fileNumber + ".txt"; // create new file name
                fileNumber++;
                if (!File.Exists(newFileName)) // check if the new file name already exists
                {
                    using (FileStream fs = File.Create(newFileName)) // create new file
                    {
                        Console.WriteLine("New file created successfully.");
                    }
                }
                else // if the new file name already exists, increment the serial number and check again
                {
                    while (File.Exists(newFileName))
                    {
                        fileNumber++;
                        newFileName = path + "\\" + baseName + fileNumber + ".txt";
                    }
                    using (FileStream fs = File.Create(newFileName)) // create new file
                    {
                        Console.WriteLine("New file created successfully.");
                    }

                }
                fileName = newFileName; // update the file name to use the new file name

            }

        }

        public static string getConnectionStringFromDB()
        {
            string SqlQuery = "SELECT  VALUE from Config where [KEY]='logFilePath'";

            string id;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                // Adapter
                using (SqlCommand command = new SqlCommand(SqlQuery, connection))
                {
                    connection.Open();
                    //Reader

                    id = command.ExecuteScalar().ToString();



                }
            }
            return id;
        }
    }
}
//public static bool CreateFile(string fileName) // function to create the file if it doesn't exist
//{
//    if (!File.Exists(fileName))
//    {
//        using (FileStream fs = File.Create(fileName))
//        {
//            Console.WriteLine("File created successfully.");
//            return true;
//        }
//    }
//    else
//    {
//        Console.WriteLine("File already exists.");
//        return false;
//    }
//}