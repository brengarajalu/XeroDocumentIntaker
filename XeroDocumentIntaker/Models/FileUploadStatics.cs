using System;
using Microsoft.AspNetCore.Http;

namespace XeroDocumentIntaker.Models
{
    public class FileUploadStatics
    {
        public long id { get; set; }
        public string UploadedBy { get; set; }
        public long fileCount { get; set; }
        public long totalfileSize { get; set; }
        public decimal totalAmount { get; set; }
        public decimal totalAmountDue { get; set; }
        public FileUploadStatics()
        {

        }
       
    }
}
