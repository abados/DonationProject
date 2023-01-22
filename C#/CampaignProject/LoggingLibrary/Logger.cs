using System;
using System.Collections.Generic;
using System.IO;
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

            // Write the log message to a file
            using (var fileStream = new FileStream("log.txt", FileMode.Append))
            using (var writer = new StreamWriter(fileStream))
            {

                writer.WriteLine($"{DateTime.Now} {level}: {message}");
                
            }
            //File.WriteAllText("log.txt", string.Empty);
            File.Copy("log.txt", "C:\\Users\\User\\source\\Projects\\Semester2\\MidProject\\Logs\\file.txt", true);
        }
        
    }
}
