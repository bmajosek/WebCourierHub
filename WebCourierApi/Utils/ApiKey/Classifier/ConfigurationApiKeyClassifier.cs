namespace WebCourierApi.Utils.ApiKey.Classifier
{
    public class ConfigurationApiKeyClassifier : IApiKeyClassifier
    {
        private readonly IEnumerable<string> _externalKeys;
        private readonly IEnumerable<string> _internalKeys;

        public ConfigurationApiKeyClassifier(IConfiguration configuration)
        {
            var apiKeyOptions = new ApiKeyOptions();
            configuration.GetSection(ApiKeyOptions.ApiKey).Bind(apiKeyOptions);
            _externalKeys = apiKeyOptions.ExternalKeys;
            _internalKeys = apiKeyOptions.InternalKeys;
        }
        public bool IsKeyExternal(string key) => _externalKeys.Contains(key);
        public bool IsKeyInternal(string key) => _internalKeys.Contains(key);
    }
}
