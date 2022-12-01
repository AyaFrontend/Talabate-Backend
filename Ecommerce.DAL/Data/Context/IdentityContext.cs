using Ecommerce.DAL.Data.IdentityConfigurations;
using Ecommerce.DAL.Entities.Identites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL.Data.Context
{
    public class IdentityContext :IdentityDbContext<AppUser>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.ApplyConfiguration(new AddressConfigurations());
            //builder.ApplyConfiguration(new AppUserConfigurations());
        }

       
    }
}
