using System;
using System.Collections.Generic;
using XeroDocumentIntaker.DataAccess;
using XeroDocumentIntaker.Models;
using System.Linq;

namespace XeroDocumentIntaker.Service
{
    public class ReportService : IReportService
    {
        private IDocumentRepository documentRepository;
        public ReportService()
        {
            this.documentRepository = new DocumentRepository(new ReportDBContext());
        }
      
        public int SaveReport(Report report)
        {
            return this.documentRepository.SaveReport(report);

        }
        public Report GetReportById(long id)
        {
   
            return this.documentRepository.GetReportById(id);
        }

        public List<FileUploadStatics> GetStatistics()
        {
            List<FileUploadStatics> uploadStatics = new List<FileUploadStatics>();
            uploadStatics = this.documentRepository.GetAllReports()
            .GroupBy(x => x.UploadedBy).Select(g => new FileUploadStatics()
            {
                id = g.Select(x=>x.Id).First(),
                UploadedBy = g.Select(x=>x.UploadedBy).First(),
                fileCount = g.Count(),
                totalfileSize = g.Sum(x => x.FileSize),
                totalAmount = g.Sum(x => x.ReportDetails.TotalAmount),
                totalAmountDue = g.Sum(x => x.ReportDetails.TotalAmountDue)
            }).ToList();
            return uploadStatics;
        }
        
       
    }
}
