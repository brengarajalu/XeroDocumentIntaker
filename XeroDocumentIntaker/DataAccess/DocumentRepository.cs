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
            this.context.Report.Add(report);
            return this.context.SaveChanges();

           
        }
        public Report GetReportById(long id)
        {
            return this.context.Report.Find(id);
        }
        public List<Report> GetAllReports()
        {

            return this.context.Report.ToList();
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
