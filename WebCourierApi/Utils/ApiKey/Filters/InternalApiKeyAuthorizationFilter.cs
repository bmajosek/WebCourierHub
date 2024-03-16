using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebCourierApi.Utils.ApiKey.Classifier;

namespace WebCourierApi.Utils.ApiKey.Filters
{
    public class InternalApiKeyAuthorizationFilter : IAuthorizationFilter
    {
        private readonly IApiKeyClassifier _checker;
        private readonly ApiKeyHttpContextReader _reader;

        public InternalApiKeyAuthorizationFilter(IApiKeyClassifier checker, ApiKeyHttpContextReader reader)
        {
            _checker = checker;
            _reader = reader;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!_reader.TryReadValue(context.HttpContext, out var apiKey) || !_checker.IsKeyInternal(apiKey))
            {
                context.Result = new ObjectResult(new { Title = "Invalid api key", Details = "This endpoint requires an api key with internal scope." })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }
        }
    }

    public class InternalApiKeyAttribute : ServiceFilterAttribute
    {
        public InternalApiKeyAttribute() : base(typeof(InternalApiKeyAuthorizationFilter))
        { }
    }
}
