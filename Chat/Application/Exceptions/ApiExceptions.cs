namespace Application.Exceptions
{
    public class ApiException : Exception
    {
        
        public ExceptionStatus StatusCode { get; private set; }

        public ApiException() { }

        public ApiException(string message, ExceptionStatus statusCode) : base(message)
        {
            StatusCode = statusCode;
        }

        public ApiException(string message, System.Exception inner) : base(message, inner) { }
    }
}
