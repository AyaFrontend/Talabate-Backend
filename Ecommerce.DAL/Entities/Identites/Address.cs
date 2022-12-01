using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecommerce.DAL.Entities.Identites
{
    public class Address
    {
        public int Id { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Country { set; get; }
        public string City { set; get; }
        public string Street { set; get; }
       
        [Required]
        public string AppUserId { set; get; }
        public AppUser AppUser { set; get; }
    }
}
