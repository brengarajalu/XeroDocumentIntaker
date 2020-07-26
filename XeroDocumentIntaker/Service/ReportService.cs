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

        public List<Report> GetAllReports()
        {
            return this.documentRepository.GetAllReports().ToList<Report>();
        }

       
    }
}
