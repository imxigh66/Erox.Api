using Erox.Api.Contracts.common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Erox.Api.Filters
{
    public class EroxExceptionHandler:ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var apiError = new ErrorResponse();
            apiError.StatusCode = 400;
            apiError.StatusPhrase = "Internal server error";
            apiError.Timestamp = DateTime.Now;
            apiError.Errors.Add(context.Exception.Message);
            context.Result=new JsonResult(apiError) { StatusCode=500};
        }
    }
}
