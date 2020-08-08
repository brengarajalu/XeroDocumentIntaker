using System;
using NUnit.Framework;
using Moq;
using XeroDocumentIntaker.DataAccess;
using XeroDocumentIntaker.Service;
using XeroDocumentIntaker.Controllers;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using XeroDocumentIntaker.Models;
using System.Collections.Generic;

namespace XeroDocumentIntaker.tests
{
    [TestFixture]
    public class UnitTest1
    {

       [SetUp]
        public void SetUp()
        {

        }


        [Test]
        public static void CreateFakeMultiPartFormData()
        {
       
            String expectedFileContents = "This is the expected file contents!";

            // Set up the form file with a stream containing the expected file contents
            Mock<IFormFile> formFile = new Mock<IFormFile>();
            formFile.Setup(ff => ff.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
              .Returns<Stream, CancellationToken>((s, ct) =>
              {
                  byte[] buffer = Encoding.Default.GetBytes(expectedFileContents);
                  s.Write(buffer, 0, buffer.Length);
                  return Task.CompletedTask;
              });

            // Set up the form collection with the mocked form
            Mock<IFormCollection> forms = new Mock<IFormCollection>();
            forms.Setup(f => f.Files[It.IsAny<int>()]).Returns(formFile.Object);

            // Create the Upload Contoller
            var mockReportService = new Mock<IReportService>();
            var uploadController = new UploadController(mockReportService.Object);

            // Set up the context
            uploadController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
            };

            //// Set up the forms
            uploadController.Request.Form = forms.Object;
            var result = uploadController.UploadFile("test@gmail.com");
            Assert.IsNotNull(result);
        }



        [Test]
        public void TestController_ShouldReturnReportForId()
        {
            var mockReportService = new Mock<IReportService>();
            var mockReportRepo = new Mock<IReportService>();
            var controller = new UploadController(mockReportService.Object);
            Report testReport = new Report { Id = 1, CreatedDate = DateTime.Now };
            mockReportService.Setup(p => p.GetReportById(1)).Returns(testReport);

            IActionResult result = controller.GetReportById(1);
            var actionResult = controller.GetReportById(1) as OkObjectResult;
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<Report>(actionResult.Value);

        }

        [Test]
        public void TestController_GetStatistics()
        {
            var mockReportService = new Mock<IReportService>();
            var mockReportRepo = new Mock<IReportService>();
            var controller = new UploadController(mockReportService.Object);
            FileUploadStatics testStats = new FileUploadStatics {
                Id = 1, TotalAmount = 100, TotalfileSize=1000,FileCount=1
            };
            mockReportService.Setup(p => p.GetStatistics()).Returns(new List<FileUploadStatics> { testStats });
            var actionResult = controller.GetStatistics() as OkObjectResult;
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<List<FileUploadStatics>>(actionResult.Value);

        }
    }
}


