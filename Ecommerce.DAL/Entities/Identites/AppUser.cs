using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.DAL.Entities.Identites
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { set; get; }
        public Address Address { set; get; }
    }
}
