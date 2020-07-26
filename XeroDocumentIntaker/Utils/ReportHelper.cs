using System;
using System.Diagnostics;
using XeroDocumentIntaker.Models;
using System.IO;

namespace XeroDocumentIntaker.Utils
{
    public static class ReportHelper
    {

        /// <summary>
        /// Convert PDF to Text
        /// </summary>
        /// <param name="filePath"></param>
        public static void ConvertPDFToText(String filePath)
        {
            try
            {
                var escapedArgs = filePath.Replace("\"", "\\\"");

                var process = new Process()
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "pdftotext",
                        Arguments = escapedArgs,
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    }
                };
                process.Start();
                string result = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //Read text from pdf converted text file 
        public static void ReadFromFile(String filePath)
        {
            // Read file using StreamReader. Reads file line by line    
            using (StreamReader file = new StreamReader(filePath))
            {
                int counter = 0;
                string ln;

                while ((ln = file.ReadLine()) != null)
                {
                    Console.WriteLine(ln);
                    counter++;
                }
                file.Close();
            }
        }



    }

   
}
