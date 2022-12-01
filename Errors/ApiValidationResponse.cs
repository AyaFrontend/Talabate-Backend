using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Errors
{
    public class ApiValidationResponse :ApiResponse
    {
        public IEnumerable<string> errors { set; get; }

        public ApiValidationResponse():base(400)
        { }
    }
}
