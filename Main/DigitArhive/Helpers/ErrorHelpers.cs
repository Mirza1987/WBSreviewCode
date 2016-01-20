using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using DigitArchive.Models;
using DigitArhive.Models;
using System.Configuration;

namespace DigitArhive.Helpers
{
    public class ErrorHelpers
    {
        public static void LogError(Exception ex, ErrorLevel errorLevel, string message = null)
        {
            using (var db = new ApplicationDbContext())
            {
                try
                {
                    var errorTable = new ErrorTable
                    {
                        AppMessage = message,
                        ErrorLevel = errorLevel.ToString(),
                        ErrorTime = DateTime.Now,
                        ExceptionMessage = ex.ToString()
                    };

                    db.ErrorsTable.Add(errorTable);
                    db.SaveChanges();
                }
                finally
                {
                    string filePath = ConfigurationManager.AppSettings["errorFilePath"];
                    if (File.Exists(filePath))
                    {
                        FileInfo fileInfo = new FileInfo(filePath);
                        if(fileInfo.Length<100000)
                        {
                            WriteInLogFile(FileMode.Append, filePath, errorLevel, message, ex);
                        }
                        else
                        {
                            string bckpFileName = filePath.Remove(filePath.Length - 4);
                            bckpFileName = bckpFileName + ".bkp";
                            File.Move(filePath, bckpFileName);
                            WriteInLogFile(FileMode.OpenOrCreate, filePath, errorLevel, message, ex);
                        }
                    }
                    else
                        WriteInLogFile(FileMode.OpenOrCreate, filePath, errorLevel, message, ex);
                }
            }

        }

        private static void WriteInLogFile(FileMode fileMode, string filePath, ErrorLevel errorLevel, string message, Exception ex)
        {
            using (var stream = new FileStream(filePath, fileMode))
            using (var stringWriter = new StreamWriter(stream))
            {
                stringWriter.WriteLine("====================Begin Error====================");
                stringWriter.WriteLine("Error level: " + errorLevel.ToString());
                stringWriter.WriteLine("Message from app: " + message);
                stringWriter.WriteLine("Exception: " + ex.ToString());
                stringWriter.WriteLine("Time: " + DateTime.UtcNow);
                stringWriter.WriteLine("====================End Error====================");
            }
        }
    }
}