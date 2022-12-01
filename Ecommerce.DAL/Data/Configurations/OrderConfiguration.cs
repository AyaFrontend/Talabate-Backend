using Ecommerce.DAL.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.DAL.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.AddressToShip, AddressToShip => AddressToShip.WithOwner());
            builder.Property(o => o.orderStatus).HasConversion(oStatus => oStatus.ToString(), oStatus =>(OrderStatus)Enum.Parse(typeof(OrderStatus), oStatus));
            builder.HasMany(o => o.orderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
