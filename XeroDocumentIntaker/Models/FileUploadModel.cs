using System;
using Microsoft.AspNetCore.Http;

namespace XeroDocumentIntaker.Models
{
    public class FileUploadModel
    {
        public string UploadedBy { get; set; }
        public FileUploadModel(String uploadedBy)
        {
            this.UploadedBy = uploadedBy;
        }
       
        //public IFormFile FormFile { get; set; }
    }
}
