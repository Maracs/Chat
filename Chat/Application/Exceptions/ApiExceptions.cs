namespace Application.Exceptions
{
    public class ApiException : Exception
    {
        
        public ExceptionStatus.Status StatusCode { get; private set; }

        public ApiException() { }

        public ApiException(string message, ExceptionStatus.Status statusCode) : base(message)
        {
            StatusCode = statusCode;
        }

        public ApiException(string message, System.Exception inner) : base(message, inner) { }
    }
}
