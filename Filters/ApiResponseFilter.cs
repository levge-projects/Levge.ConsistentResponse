using Levge.ConsistentResponse.Interfaces;
using Levge.ConsistentResponse.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Levge.ConsistentResponse.Filters
{
    public class ApiResponseFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Response.HasStarted)
                return;

            if (context.Result is ObjectResult objectResult && objectResult.Value is not IApiResponse)
            {
                context.Result = new ObjectResult(ApiResponse<object>.Success(objectResult.Value))
                {
                    StatusCode = objectResult.StatusCode ?? StatusCodes.Status200OK
                };
            }
            else if (context.Result is UnauthorizedResult)
            {
                context.Result = new ObjectResult(ApiResponse<object>.Fail("Unauthorized access"))
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }
            else if (context.Result is ForbidResult)
            {
                context.Result = new ObjectResult(ApiResponse<object>.Fail("You don't have permission to access this resource."))
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
            }
            else if (context.Result is StatusCodeResult statusCodeResult)
            {
                context.Result = new ObjectResult(ApiResponse<object>.Success(null))
                {
                    StatusCode = statusCodeResult.StatusCode
                };
            }
            else if (context.Result == null)
            {
                context.Result = new ObjectResult(ApiResponse<object>.Success(null))
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
        }
    }
}
