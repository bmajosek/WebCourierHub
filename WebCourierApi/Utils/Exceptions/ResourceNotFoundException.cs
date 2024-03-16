namespace WebCourierApi.Utils.Exceptions
{
    public class ResourceNotFoundException : Exception, IInformativeError
    {
        public string Title => "Resource not found";
        public string Details => "Requested resource or subject of requested action was not found.";
        public int HttpStatusCode => StatusCodes.Status404NotFound;
    }
}
