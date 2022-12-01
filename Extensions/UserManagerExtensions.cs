using Ecommerce.DAL.Data.Context;
using Ecommerce.DAL.Entities.Identites;
using Ecoomerce.BLL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Extensions
{
    public static class UserManagerExtensions
    {
       
        public static async  Task<AppUser> FindByEmailEagerAsync(this UserManager<AppUser> userManager , string email)
        {
           return await userManager.Users.Where(a => a.Email == email).Include(a => a.Address).SingleOrDefaultAsync();
        }
    }
}
