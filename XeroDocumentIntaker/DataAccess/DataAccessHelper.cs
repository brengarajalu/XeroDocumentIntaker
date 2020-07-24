using System;
using XeroDocumentIntaker.Models;

namespace XeroDocumentIntaker.DataAccess
{
    public class DataAccessHelper : IDataAccess
    {

        public int SaveReport(Report report)
        {
            using (var db = new ReportDBContext())
            {
                db.Report.Add(report);
                return db.SaveChanges();

            }
        }
        public Report GetReportById(long id)
        {
            using (var db = new ReportDBContext())
            {
                return db.Report.Find(id);
            }
        }
        public System.Linq.IQueryable<Report> GetAllReports()
        {
            using (var db = new ReportDBContext())
            {
                return db.Report.AsQueryable();
            }
        }

       
    }
}
