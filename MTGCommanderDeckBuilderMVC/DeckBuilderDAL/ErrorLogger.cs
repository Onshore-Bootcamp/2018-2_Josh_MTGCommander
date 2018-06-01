using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeckBuilderDAL
{
    public class ErrorLogger
    {
        //Dependencies
        private string logPath;

        //Constructor
        public ErrorLogger(string filePath)
        {
            logPath = filePath;
        }

        public void ErrorLogging(string level, string className, 
            string methodName, string message, string stackTrace = null)
        {
            DateTime currentDateTime = DateTime.Now;

            try
            {

                using (StreamWriter errorLogger = new StreamWriter(logPath, true))
                {
                    errorLogger.WriteLine(new string('-', 60));
                    errorLogger.WriteLine($"{currentDateTime.ToString("MM/dd/yyyy HH:mm:ss")} - {level} - {className} - {methodName}");
                    errorLogger.WriteLine(message);
                    if (!string.IsNullOrWhiteSpace(stackTrace))
                    {
                        errorLogger.WriteLine(stackTrace);
                    }


                }
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }
    }
}
