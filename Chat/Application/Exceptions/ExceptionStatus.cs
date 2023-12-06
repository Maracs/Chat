using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public static class ExceptionStatus
    {
        public enum Status
        {
            NotFound = 404,
            Unauthorized = 401,
            BadRequest = 400
        }
    }
}
