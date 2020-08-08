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
        public IActionResult UploadFile([FromForm] string UploadedBy)
        {
            var request = HttpContext.Request;

            if(String.IsNullOrEmpty(UploadedBy))
            {
                return BadRequest("UploadedBy cannot be empty");
            }
            if (request.Form.Files.Count > 0)
            {
                IFormFile file = request.Form.Files.First();
                String path = Path.Combine(Directory.GetCurrentDirectory(),file.FileName);
                String outputPath = Path.Combine(Directory.GetCurrentDirectory(), "converted.txt");
                using (Stream st = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(st);
                }
                //Convert PDF to Text
                ReportHelper.ConvertPDFToText(path + "  " + outputPath);
                // Extract report fields
                Report rep = ReportHelper.ExtractReport(outputPath, UploadedBy);

                //Save report
                _reportService.SaveReport(rep);

                return Ok("file uploaded successfully");
            }
            return BadRequest("Invalid file");

        }

        /// <summary>
        /// Get Report by report Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Route("document")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetReportById(long id)
        {
            if(id < 1)
            {
                return BadRequest("Invalid report Id");
            }
            return Ok(this._reportService.GetReportById(id));
        }

        /// <summary>
        /// Return report statistics
        /// </summary>
        /// <returns></returns>
        [Route("stats")]
        public IActionResult GetStatistics()
        {
            return Ok(this._reportService.GetStatistics());
        }

      
    }
} 
