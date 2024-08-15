using Erox.Api.Contracts.common;
using Erox.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Erox.Api.Filters
{
    public class ValidateModelAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(!context.ModelState.IsValid) 
            {
                var apiError = new ErrorResponse();
                apiError.StatusCode = 400;
                apiError.StatusPhrase = "Bad request";
                apiError.Timestamp = DateTime.Now;
                var errors=context.ModelState.AsEnumerable();

                foreach(var error in errors) 
                {
                    foreach (var inner in error.Value.Errors)
                    {
                        apiError.Errors.Add(inner.ErrorMessage);
                    }

                }
                context.Result=new BadRequestObjectResult(apiError);
               
            }
        }
    }
}
