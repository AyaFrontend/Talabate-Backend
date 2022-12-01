using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Errors
{
    public class ApiResponse
    {
        public int StatusCode { set; get; }
        public string Message { set; get; }

        public ApiResponse(int statusCode , string message = null)
        {
            this.StatusCode = statusCode;
            this.Message = message ?? getErrorMessage(statusCode);
        }

        private string getErrorMessage(int statusCode)
            => statusCode switch
            {
                400 => "A badReques ",
                401 => "Authorized , you are not",
                404 => "Resource not found",
                500 => "Server Error",
                _ => null
            };


    }
}
