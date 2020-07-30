using System;
using XeroDocumentIntaker.Models;
using System.Linq;
using System.Collections.Generic;

namespace XeroDocumentIntaker.DataAccess
{
    public class DocumentRepository : IDocumentRepository, IDisposable
    {
        private ReportDBContext context;

        public DocumentRepository(ReportDBContext context)
        {
            this.context = context;
        }
        public int SaveReport(Report report)
        {
            report.CreatedDate = DateTime.Now;
            this.context.Report.Add(report);
            return this.context.SaveChanges();
            //foreach (ReportDetail rd in report.ReportDetails)
            //{
            //    this.context.ReportDetail.Add(rd);
            //}
            //return this.context.SaveChanges();

           
        }
        public Report GetReportById(long id)
        {
            Report rep =  this.context.Report.Find(id);
            rep.ReportDetails = this.context.ReportDetail.Where(x => x.ReportId == id).First();
            return rep;
        }
        public List<Report> GetAllReports()
        {
            List<Report> lstReport = this.context.Report.ToList();
            foreach(Report rep in lstReport)
            {
                rep.ReportDetails = this.context.ReportDetail.Where(x => x.ReportId == rep.Id).First();
            }
            
            return lstReport;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
