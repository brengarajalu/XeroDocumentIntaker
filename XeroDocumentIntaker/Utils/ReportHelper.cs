using System;
using System.Diagnostics;
using XeroDocumentIntaker.Models;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace XeroDocumentIntaker.Utils
{
    /// <summary>
    /// Helper utility for report extraction
    /// </summary>
    public static class ReportHelper
    {

        /// <summary>
        /// Convert PDF to Text
        /// </summary>
        /// <param name="filePath"></param>
        public static bool ConvertPDFToText(String filePath)
        {
            bool isSuccess = false;
            try
            {
                var escapedArgs = filePath.Replace("\"", "\\\"");

                var process = new Process()
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = Constants.PDF_TO_TEXT_COMMAND,
                        Arguments = escapedArgs + "  " + "-layout",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    }
                };
                process.Start();
                string result = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                isSuccess = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSuccess;
        }

        /// <summary>
        /// Extract report from file path
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="uploadedBy"></param>
        /// <returns></returns>
        public static Report ExtractReport(String filePath, String uploadedBy)
        {
            Report report = new Report();
            try
            { 
         
                report.UploadedBy = uploadedBy;
                ReadFromFile(report, filePath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return report;

        }

        /// <summary>
        /// Parse columnar data to extract specific fiellds
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private static String[] ParseTabularData(String line)
        {
            return System.Text.RegularExpressions.Regex.Split(line, @"\s{2,}");
        }


        //Read text from pdf converted text file 
        private static void ReadFromFile(Report report, String filePath)
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
                 
                        if (prevline == Constants.INVOICE)
                        {
                            rep.Vendor = ln;
                            prevline = "";
                        }
                        //First Extract fields by line position
                        if (ln.StartsWith(Constants.INVOICE) && !ln.Contains("Invoice Receipt"))
                        {
                            rep.InvoiceDate = ParseTabularData(ln)[1];
                            prevline = Constants.INVOICE;

                        }
                        // Extract by prefix
                        if (ln.StartsWith(Constants.TAX_PREFIXED))
                        {
                            //tax
                            String[] cols = ParseTabularData(ln);
                            String taxString = Regex.Match(cols[1], @"\d+.+\d").Value;
                            Decimal.TryParse(taxString, out decimal result);
                            rep.Tax = result;

                        }
                        if (ln.StartsWith(Constants.PAID))
                        {
                            //Amount paid
                            String[] cols = ParseTabularData(ln);
                            String taxString = Regex.Match(cols[1], @"\d+.+\d").Value;
                            Decimal.TryParse(taxString, out decimal result);
                            rep.TotalAmount = result;
                            prevline =  "Paid";

                        }
                        if (prevline == Constants.PAID)
                        {
                            //Amount paid
                            String[] cols = ParseTabularData(ln);
                            String taxString = Regex.Match(cols[0], @"\d+.+\d").Value;
                            Decimal.TryParse(taxString, out decimal result);
                            rep.TotalAmountDue = result;
                            prevline = "";

                        }

                        if (prevline == Constants.TOTAL_DUE)
                        {
                            rep.Currency = ln;
                            prevline = "";
                        }

                        if (ln.StartsWith(Constants.TOTAL_DUE))
                        {
                            prevline = Constants.TOTAL_DUE;
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
