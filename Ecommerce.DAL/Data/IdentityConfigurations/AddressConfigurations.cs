using Ecommerce.DAL.Entities.Identites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.DAL.Data.IdentityConfigurations
{
    public class AddressConfigurations : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(a => a.Id);
              
            builder.HasOne(a => a.AppUser).WithOne(u => u.Address).HasForeignKey<Address>(a => a.AppUserId);
        }
    }
}
