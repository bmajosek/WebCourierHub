namespace WebCourierHub.Support.ApiConfig
{
    public interface IApiConfig
    {
        string Url { get; }
        void AddCredentialsTo(HttpClient client);
    }
}
