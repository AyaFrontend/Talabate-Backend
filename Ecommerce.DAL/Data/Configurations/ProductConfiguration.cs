using Ecommerce.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.DAL.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property("Name").IsRequired();
            builder.Property(p => p.Description).IsRequired();

            builder.Property(p => p.Price).IsRequired().HasColumnType("decimal(18,2)");
            builder.HasOne(p => p.ProductBrand).WithMany(b=>b.Products).HasForeignKey(p => p.ProductBrandId);

            builder.HasOne(p => p.ProductType).WithMany(t=>t.Products).HasForeignKey(p=>p.ProductTypeId);
        }
    }
}
