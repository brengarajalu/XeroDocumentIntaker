using System;
namespace XeroDocumentIntaker.ErrorHandler
{
   /// <summary>
   /// Exception wrapper to handle exceptions while generating report
   /// </summary>
    public class ReportGenerationException : Exception
    {

        public ReportGenerationException(Exception ex)
        {
            this.Value = ex;
        }
        public int Status { get; set; } = 500;

        public object Value { get; set; }
    }
}
