
namespace WebCourierHub.Support.ApiConfig
{
    public class InternalApiConfig : IApiConfig
    {
        private readonly string _url;
        private readonly string _apiKeyHeaderName;
        private readonly string _apiKey;

        public InternalApiConfig(IConfiguration configuration)
        {
            _url = configuration["InternalApi:Url"]!;
            _apiKeyHeaderName = configuration["InternalApi:HeaderName"]!;
            _apiKey = configuration["InternalApi:Key"]!;
        }

        public string Url => _url;

        public void AddCredentialsTo(HttpClient client)
        {
            client.DefaultRequestHeaders.Add(_apiKeyHeaderName, _apiKey);
        }
    }
}
