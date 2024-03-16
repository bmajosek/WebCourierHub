namespace WebCourierApi.Utils.Exceptions
{
    public class InvalidActionException : Exception, IInformativeError
    {
        public InvalidActionException(string details) => Details = details;
        public string Title => "Invalid action attepted";
        public string Details { get; init; }
        public int HttpStatusCode => StatusCodes.Status409Conflict;
    }
}
