using Ecommerce.DAL.Entities.Identites;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL.Data.Context
{
    public class IdentityContextDataSeed
    {
        public static async Task SeedingUser(UserManager<AppUser> _userManager)
        {
            var user = new AppUser()
            {
                Email = "aya@gmail.com",
                DisplayName = "Aya Mohamed",
                PhoneNumber = "01028330432",
                UserName ="aya",
                Address = new Address()
                {
                    FirstName ="Aya",
                    LastName ="Mohamed",
                    City="minya",
                    Country="Egypt",
                    Street = "EL Salam"
                }


            };

            if (!await _userManager.Users.AnyAsync())
                await _userManager.CreateAsync(user, "Pa$$w0rd");

        }
    }
}
