using System;
namespace XeroDocumentIntaker.Models
{
    public class ReportDetail
    {
        public ReportDetail()
        {
        }
        public long Id { get; set; }
        public long ReportId { get; set; }
        public String Vendor { get; set; }
        public String InvoiceDate { get; set; }
        public Decimal TotalAmount { get; set; }
        public Decimal TotalAmountDue { get; set; }
        public String Currency { get; set; }
        public Decimal Tax { get; set; }

    }
}
