namespace WebCourierApi.Utils.ApiKey.Classifier
{
    public interface IApiKeyClassifier
    {
        bool IsKeyExternal(string key);
        bool IsKeyInternal(string key);
    }
}
