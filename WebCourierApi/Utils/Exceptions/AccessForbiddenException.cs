namespace WebCourierApi.Utils.Exceptions
{
    public class AccessForbiddenException : Exception, IInformativeError
    {
        public string Title => "Access forbidden";

        public string Details => "Access to requested resource is beyond the scope of provided api key.";

        public int HttpStatusCode => StatusCodes.Status403Forbidden;
    }
}
