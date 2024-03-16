namespace WebCourierApi.Utils.Exceptions
{
    public class UnexpectedErrorException : Exception, IInformativeError
    {
        public string Title => "Unexpected error";
        public string Details => "An unexpected error occured. Please try again.";
        public int HttpStatusCode => StatusCodes.Status500InternalServerError;
    }
}
