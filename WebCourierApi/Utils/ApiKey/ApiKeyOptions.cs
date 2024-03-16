namespace WebCourierApi.Utils.ApiKey
{
    public class ApiKeyOptions
    {
        public const string ApiKey = "ApiKey";
        public string HeaderName { get; set; } = string.Empty;
        public string[] ExternalKeys { get; set; } = [];
        public string[] InternalKeys { get; set; } = [];
    }
}
