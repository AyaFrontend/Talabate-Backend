using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.DTO
{
    public class AddressDto
    {
        [Required]
        public string FirstName { set; get; }

        [Required]
        public string LastName { set; get; }

        [Required]
        public string Country { set; get; }

        [Required]
        public string City { set; get; }

        [Required]
        public string Street { set; get; }

    
    }
}
