using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace XeroDocumentIntaker.ErrorHandler
{
    /// <summary>
    /// Global exception handler for exceptions
    /// </summary>
    public class ReportExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; set; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is ReportProcessingException rpexception)
            {
                context.Result = new ObjectResult(rpexception.Value)
                {
                    StatusCode = rpexception.Status,
                };
                context.ExceptionHandled = true;
            }

            if (context.Exception is ReportGenerationException rgexception)
            {
                context.Result = new ObjectResult(rgexception.Value)
                {
                    StatusCode = rgexception.Status,
                };
                context.ExceptionHandled = true;
            }
        }
    }
}
