using Ecommerce.DAL.Entities.Identites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.DTO
{
    public class RegisterDto
    {
        public string DisplayName { set; get; }
        public string Password { set; get; }
        public string Email { set; get; }
        public string PhoneNumber { set; get; }
  
        public string Country { set; get; }
        public string City { set; get;}
        public string Street { set; get; }
    }
}
