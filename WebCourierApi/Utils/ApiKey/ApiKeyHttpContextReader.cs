
namespace WebCourierApi.Utils.ApiKey
{
    public class ApiKeyHttpContextReader
    {
        private readonly string _headerKeyName;

        public ApiKeyHttpContextReader(IConfiguration configuration)
        {
            var apiKeyOptions = new ApiKeyOptions();
            configuration.GetSection(ApiKeyOptions.ApiKey).Bind(apiKeyOptions);
            _headerKeyName = apiKeyOptions.HeaderName;
        }

        public bool TryReadValue(HttpContext httpContext, out string value)
        {
            value = httpContext?.Request?.Headers?[_headerKeyName].SingleOrDefault(string.Empty) ?? string.Empty;
            return !string.IsNullOrEmpty(value);
        }
    }
}
