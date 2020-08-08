using System;
using System.Collections.Generic;
using XeroDocumentIntaker.Models;

namespace XeroDocumentIntaker.DataAccess
{
    public interface IDocumentRepository
    {
        public int SaveReport(Report report);
        public Report GetReportById(long id);
        public List<Report> GetReportStatistics();
    }
}
