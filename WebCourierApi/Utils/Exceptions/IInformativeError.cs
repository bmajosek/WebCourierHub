namespace WebCourierApi.Utils.Exceptions
{
    public interface IInformativeError
    {
        public string Title { get; }
        public string Details { get; }
        public int HttpStatusCode { get; }
    }
}
