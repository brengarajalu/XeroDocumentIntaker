
using System;
using NUnit.Framework;
using System.IO;
using XeroDocumentIntaker.Models;
using System.Collections.Generic;
using XeroDocumentIntaker.Utils;
using Microsoft.DotNet.InternalAbstractions;

namespace XeroDocumentIntaker.tests
{
    [TestFixture]
    public class UtilsTest
    {

        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void ReportHelper_ShouldConvertPDF()
        { 
           Boolean result = ReportHelper.ConvertPDFToText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "HubdocInvoice1.pdf"));
           Assert.AreEqual(result, true);

        }
        [Test]
        public void ReportHelper_ShouldExtractPDF()
        {
            Report report = ReportHelper.ExtractReport(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "HubdocInvoice1.txt"), "tester");
            Assert.IsNotNull(report.ReportDetails);
            Assert.AreEqual(report.UploadedBy, "tester");
            

        }


    }
}


