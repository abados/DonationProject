using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggingLibrary
{
    internal class LogFile: ILoggerUtilities
    {
       

        public static string connectionString = System.IO.File.ReadAllText("connectionStr.txt").Replace("\\\\", "\\").Trim();
        private static string fileName = getLogFilePathFromDB().Replace("\\\\", "\\").Trim();
        private static string originalPath = fileName;
        private static string path;
        private static string baseName;
        private static string newFileName;
        private static int fileNumber = 1;
        private static readonly object _lock = new object();
        private static int MAXSIZE = 1024 * 1024; //1 MEGA

        public void Init()
        {
            
            Task.Run(() =>
            {
                while (true)
                {
                    if (Logger.LogItemsQueue.Count > 0)
                    {
                        LogItem item = Logger.LogItemsQueue.Dequeue();
                        WriteToFile(item);
                        System.Threading.Thread.Sleep(11000);

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
            LogItem log = new LogItem();
            log.Message = msg;
            log.DateTime= DateTime.Now;
            Logger.LogItemsQueue.Enqueue(log);
        }

        public void LogException(string msg, Exception exception)
        {
            LogItem log = new LogItem();
            log.Exception = exception;
            log.Message = msg;
            log.DateTime= DateTime.Now;
            Logger.LogItemsQueue.Enqueue(log);
            
        }

        public void LogCheckHoseKeeping()
        {
            FileInfo file = new FileInfo(fileName);
            CheckSize(file);
        }

        public void LogError(string msg)
        {
            LogItem log = new LogItem();
            log.Message = msg;
            log.DateTime = DateTime.Now;
            Logger.LogItemsQueue.Enqueue(log);
        }



        public static string getLogFilePathFromDB()
        {
            string SqlQuery = "SELECT  VALUE from Config where [KEY]='logFilePath'";

            string filePath;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                // Adapter
                using (SqlCommand command = new SqlCommand(SqlQuery, connection))
                {
                    connection.Open();
                    //Reader

                    filePath = command.ExecuteScalar().ToString();

                }
            }
            return filePath;
        }

        public void WriteToFile(LogItem logItem)  // function to write text to the file
        {

            string msg = $"{logItem.DateTime}: {logItem.Message} {logItem.Exception}";

            using (StreamWriter sw = File.AppendText(fileName))
            {
                sw.WriteLine(msg);
            }
        }

        public void CheckSize(FileInfo file)  // function to write text to the file
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
    }
}

/*
        public void Log(string message)
        {

            Task.Run(() => {

                path = Path.GetDirectoryName(fileName);// get the directory path of the file

                baseName = Path.GetFileNameWithoutExtension(originalPath); //get the base name of the file (without extension or numbers)

                // Write the log message to a file
                lock (_lock)
                {

                    string text = $"{DateTime.Now} : {message}";
                    WriteToFile(text);

                }
            });


        }
*/
