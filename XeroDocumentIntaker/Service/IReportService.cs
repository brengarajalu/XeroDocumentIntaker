using System;
using System.Collections.Generic;
using XeroDocumentIntaker.Models;

namespace XeroDocumentIntaker.Service
{
    public interface IReportService
    {

          public int SaveReport(Report report);
          public Report GetReportById(long id);
          public List<FileUploadStatics> GetStatistics();
        
    }
}
