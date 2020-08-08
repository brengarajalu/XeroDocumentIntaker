using System;
using XeroDocumentIntaker.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace XeroDocumentIntaker.DataAccess
{
    public class DocumentRepository : IDocumentRepository, IDisposable
    {
        private readonly ReportDBContext _context;

        public DocumentRepository(ReportDBContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Save report object to datastore
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        public int SaveReport(Report report)
        {
            report.CreatedDate = DateTime.Now;
            this._context.Report.Add(report);
            return this._context.SaveChanges();
        }

        /// <summary>
        /// Get report by report 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Report GetReportById(long id)
        {
            Report rep =  this._context.Report.Find(id);
            rep.ReportDetails = this._context.ReportDetail.Where(x => x.ReportId == id).FirstOrDefault();
            return rep;
        }

        /// <summary>
        /// Get Report Statistics
        /// </summary>
        /// <returns></returns>
        public List<Report> GetReportStatistics()
        {
            List<Report> lstReport = this._context.Report.ToList();
            foreach(Report rep in lstReport)
            {
                rep.ReportDetails = this._context.ReportDetail.Where(x => x.ReportId == rep.Id).FirstOrDefault();
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
                    _context.Dispose();
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
