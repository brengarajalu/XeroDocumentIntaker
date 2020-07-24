using System;
using System.Diagnostics;
using XeroDocumentIntaker.Models;

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
            var escapedArgs = filePath.Replace("\"", "\\\"");

            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "pdftotext",
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

        }

        public static Report ExtractFields(String filePath)
        {
            Report rep = new Report();
            return rep;

        }


    }

   
}
