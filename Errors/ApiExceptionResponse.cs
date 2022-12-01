using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Errors
{
    public class ApiExceptionResponse : ApiResponse
    {
        public string Details { set; get; }
        
        public ApiExceptionResponse (int statusCode , string details = null , string message = null )
                                     :base(statusCode , message)
        {
            Details = details;
        }
    }
}
