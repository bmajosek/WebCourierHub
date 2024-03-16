using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace WebCourierApi.Utils.Exceptions
{
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order => int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is IInformativeError informativeError)
            {
                context.Result = new ObjectResult(new { informativeError.Title, informativeError.Details })
                {
                    StatusCode = informativeError.HttpStatusCode
                };

                context.ExceptionHandled = true;
            }
        }
    }
}
