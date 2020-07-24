using System;
using System.Collections.Generic;
using XeroDocumentIntaker.DataAccess;
using XeroDocumentIntaker.Models;
using System.Linq;

namespace XeroDocumentIntaker.ClientApp.Service
{
    public class ReportService 
    {
        private readonly IDataAccess _dataAccess;
        public ReportService(IDataAccess dataAccess) 
        {
            this._dataAccess = dataAccess;

        }

        public int SaveReport(Report report)
        {
            return _dataAccess.SaveReport(report);

        }
        public Report GetReportById(long id)
        {
            return _dataAccess.GetReportById(id);
        }

        public List<Report> GetAllReports()
        {
            return _dataAccess.GetAllReports().ToList<Report>();
        }
    }
}
