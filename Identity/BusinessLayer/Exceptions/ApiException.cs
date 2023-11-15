using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Exeptions
{
    public class ApiException:Exception
    {
        public  enum ExceptionStatus
        {
            NotFound = 404,
            Unauthorized = 401,
            BadRequest = 400
        }
       
        public ExceptionStatus StatusCode { get; private set; }

        public ApiException() { }

        public ApiException(string message, ExceptionStatus statusCode) : base(message)
        {
            StatusCode = statusCode;
        }

        public ApiException(string message, System.Exception inner) : base(message, inner) { }

        protected ApiException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
