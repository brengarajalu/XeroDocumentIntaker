using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace XeroDocumentIntaker.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UploadfilesController : ControllerBase
	{
		

		[HttpPost]
		public IActionResult UploadForm()
		{

			var request = HttpContext.Request;

			if (request.Form.Files.Count > 0)
			{
				 return Ok("file uploaded successfully");


			}
			return BadRequest("File size too large");
		}
	}
}
