using System;
using System.Collections.Generic;
using XeroDocumentIntaker.DataAccess;
using XeroDocumentIntaker.Models;
using System.Linq;
using XeroDocumentIntaker.ErrorHandler;
using System.Data.Entity;

namespace XeroDocumentIntaker.Service
{
    public class ReportService : IReportService
    {
        //Inject repository
        private IDocumentRepository _documentRepository;
        public ReportService(IDocumentRepository repository)
        {
   
            this._documentRepository = repository;
        }

        /// <summary>
        /// Save report object to datastore
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        public int SaveReport(Report report)
        {
            try
            {
                return this._documentRepository.SaveReport(report);
            }
            catch(Exception ex)
            {
                throw new ReportProcessingException(ex);
            }

        }

        /// <summary>
        /// Get report by report Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Report GetReportById(long id)
        {

            try
            {
                return this._documentRepository.GetReportById(id);
            }
            catch (Exception ex)
            {
                throw new ReportGenerationException(ex);
            }

        }

        /// <summary>
        /// Generate report statistics by grouping report by UploadedBy 
        /// </summary>
        /// <returns></returns>
        public List<FileUploadStatics> GetStatistics()
        {
            List<FileUploadStatics> uploadStatics = new List<FileUploadStatics>();
            try
            {
                uploadStatics = this._documentRepository.GetReportStatistics()
                .GroupBy(x => x.UploadedBy)
                .Select(g => new FileUploadStatics()
                {
                    Id = g.Select(x => x.Id).First(),
                    UploadedBy = g.Select(x => x.UploadedBy).First(),
                    FileCount = g.Count(),
                    TotalfileSize = g.Sum(x => x.FileSize),
                    TotalAmount = g.Sum(x => x.ReportDetails.TotalAmount),
                    TotalAmountDue = g.Sum(x => x.ReportDetails.TotalAmountDue)
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new ReportGenerationException(ex);
            }
            return uploadStatics;
        }
        
       
    }
}
