using System;
using System.Collections.Generic;
using XeroDocumentIntaker.Models;

namespace XeroDocumentIntaker.DataAccess
{
    public interface IDataAccess
    {
        public int SaveReport(Report report);
        public Report GetReportById(long id);
        public System.Linq.IQueryable<Report> GetAllReports();
    }
}
