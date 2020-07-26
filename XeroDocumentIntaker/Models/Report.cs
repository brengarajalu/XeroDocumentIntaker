using System;
namespace XeroDocumentIntaker.Models
{
    public class Report
    {
        public Report(String createdBy)
        {
            this.UploadedBy = createdBy;
        }
        public Report() {
        }
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public String UploadedBy { get; set; }
        public System.Collections.Generic.List<ReportDetail> reportDetails { get; set; }
    }
}
