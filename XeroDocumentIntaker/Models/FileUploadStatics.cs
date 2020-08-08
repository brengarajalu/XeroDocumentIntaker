using System;
using Microsoft.AspNetCore.Http;

namespace XeroDocumentIntaker.Models
{
    /// <summary>
    /// Model for Statistics
    /// </summary>
    public class FileUploadStatics
    {
        public long Id { get; set; }
        public string UploadedBy { get; set; }
        public long FileCount { get; set; }
        public long TotalfileSize { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalAmountDue { get; set; }
        public FileUploadStatics()
        {

        }
       
    }
}
