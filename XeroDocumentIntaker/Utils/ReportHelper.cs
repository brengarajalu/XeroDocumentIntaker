using System;
using System.Diagnostics;
using XeroDocumentIntaker.Models;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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
                        Arguments = escapedArgs + "  " + "-layout",
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


        public static Report ExtractReport(String filePath, String uploadedBy)
        {
            Report rep = new Report();
            rep.ReportDetails = new ReportDetail();
            rep.UploadedBy = uploadedBy;
            ReadFromFile(rep,filePath);
            return rep;

        }

        private static String[] ParseTabularData(String line)
        {
            return System.Text.RegularExpressions.Regex.Split(line, @"\s{2,}");
        }


        //Read text from pdf converted text file 
        public static void ReadFromFile(Report report, String filePath)
        {
            ReportDetail rep = new ReportDetail();

            // Read file using StreamReader. Reads file line by line    
            using (StreamReader file = new StreamReader(filePath))
            {
                report.FileSize = file.BaseStream.Length;
                int lineCount = 0;
                string ln;
                String prevline = String.Empty;
                while ((ln = file.ReadLine()) != null)
                {
                    ln = ln.Trim();
                    if (ln != "")
                    {
                        if (lineCount == 3)
                        {
                            rep.InvoiceDate = ParseTabularData(ln)[1];
                        }
                        if (lineCount == 4)
                        {
                            rep.Vendor = ln;
                        }

                        if (ln.StartsWith("Tax"))
                        {
                            //tax
                            String[] cols = ParseTabularData(ln);
                            String taxString = Regex.Match(cols[1], @"\d+.+\d").Value;
                            Decimal.TryParse(taxString, out decimal result);
                            rep.Tax = result;

                        }
                        if (ln.StartsWith("Paid"))
                        {
                            //tax
                            String[] cols = ParseTabularData(ln);
                            String taxString = Regex.Match(cols[1], @"\d+.+\d").Value;
                            Decimal.TryParse(taxString, out decimal result);
                            rep.TotalAmount = result;
                            prevline =  "Paid";

                        }
                        if (prevline == "Paid")
                        {
                            //tax
                            String[] cols = ParseTabularData(ln);
                            String taxString = Regex.Match(cols[0], @"\d+.+\d").Value;
                            Decimal.TryParse(taxString, out decimal result);
                            rep.TotalAmountDue = result;
                            prevline = "";

                        }

                        if (prevline == "Total Due")
                        {
                            rep.Currency = ln;
                            prevline = "";
                        }

                        if (ln.StartsWith("Total Due"))
                        {
                            prevline = "Total Due";
                        } 
                  
                        lineCount++;

                    }
                   
                }
                file.Close();
                report.ReportDetails = rep;
              
            }
        }



    }

   
}
