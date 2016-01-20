using DigitArchive.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DigitArhive.Helpers
{
    class FileMover
    {
        /// <summary>
        /// Move all selected files
        /// </summary>
        /// <param name="files"></param>
        /// <param name="documentId"></param>
        /// <param name="companyName"></param>
        /// <param name="documentType"></param>
        /// <param name="documentDescription"></param>
        public static void MoveFile(HttpFileCollectionBase files, int documentId, string companyName, string documentType, string documentDescription)
        {
            string fileDestinationPath = ConfigurationManager.AppSettings["pdfFileLocation"];  //test lokacija!!!!
            string finalFileDestination = String.Format("{0}\\{1}", fileDestinationPath, companyName.Replace(" ",""));

            if (!Directory.Exists(finalFileDestination))
            Directory.CreateDirectory(finalFileDestination);

            List<string> filePathList = new List<string>();

            for(int i=0; i<files.Count; i++)
            {
                filePathList.Add(files[i].FileName);
            }

            filePathList.Sort();
            int pageNumber = 1;

            foreach(var filePath in filePathList)
        {
                
                var extension = Path.GetExtension(filePath);
                var fileName = Guid.NewGuid().ToString();
                var finalFilePath = String.Format("{0}\\{1}{2}",finalFileDestination,fileName,extension);

                try
                {
                    File.Move(filePath, finalFilePath);
                }
                catch (Exception ex)
                {
                    ErrorHelpers.LogError(ex, ErrorLevel.Error, "Error on write in database");
                }
                
                Page.Create(finalFilePath, documentId, pageNumber);
                pageNumber += 1;
            }
        }


        public static void RemovePage(string filePath)
        {
            File.Delete(filePath);
        }
    }
}
