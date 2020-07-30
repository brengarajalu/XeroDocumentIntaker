using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XeroDocumentIntaker.Models;
using XeroDocumentIntaker.Service;
using XeroDocumentIntaker.Utils;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace XeroDocumentIntaker.Controllers
{
    [Route("api/[controller]")]
    public class UploadController : Controller
    {
        private readonly IReportService _reportService;
        public UploadController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpPost]
        [Route("upload")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequestFormLimits(KeyLengthLimit =100000000)]
        public IActionResult UploadFile([FromForm] string UploadedBy)
        {
            var request = HttpContext.Request;

            if (request.Form.Files.Count > 0)
            {
                IFormFile file = request.Form.Files.First();
                String path = Path.Combine(Directory.GetCurrentDirectory(),file.FileName);
                String outputPath = Path.Combine(Directory.GetCurrentDirectory(), "converted.txt");
                using (Stream st = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(st);
                }
                ReportHelper.ConvertPDFToText(path + "  " + outputPath);
                Report rep = ReportHelper.ExtractReport(outputPath, UploadedBy);
                _reportService.SaveReport(rep);


                return Ok("file uploaded successfully");
            }
            return BadRequest("Not a valid file");

        }

        [HttpGet("{id}")]
        [Route("document")]
        public Report GetReportById(long id)
        {
            return this._reportService.GetReportById(id);
        }

        [Route("stats")]
        public List<FileUploadStatics> GetStatistics()
        {
            return this._reportService.GetStatistics();
        }

      
    }
}
