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
                using (Stream st = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(st);
                }
                ReportHelper.ConvertPDFToText(path);



                return Ok("file uploaded successfully");
            }
            return BadRequest("Not a valid file");

        }


        // GET api/values/5
        [HttpGet("{id}")]
        [Route("document")]
        public Report GetReportById(long id)
        {
            return this._reportService.GetReportById(id);
        }

        // POST api/values
        [HttpPost]
        [Route("test")]
        public IActionResult Post([FromForm]string value)
        {
            return Ok(value);

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
