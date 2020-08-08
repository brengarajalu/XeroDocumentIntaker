using System;
namespace XeroDocumentIntaker.ErrorHandler
{
   /// <summary>
   /// Exception wrapper for exceptions while uploading report PDF
   /// </summary>
    public class ReportProcessingException : Exception
    {

        public ReportProcessingException(Exception ex)
        {
            this.Value = ex;
        }
        public int Status { get; set; } = 500;

        public object Value { get; set; }
    }
}
