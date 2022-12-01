using Ecommerce.DAL.Data.Configurations;
using Ecommerce.DAL.Entities;
using Ecommerce.DAL.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Ecommerce.DAL.Data.Context
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options):base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //modelBuilder.ApplyConfiguration(new ProductBrandConfiguration());
            //modelBuilder.ApplyConfiguration(new ProductTypeConfiguration());
            //modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }


        public DbSet<Product> Products { set; get; }
        public DbSet<ProductBrand> ProductBrands { set; get; }
        public DbSet<ProductType> ProductTypes { set; get; }
        public DbSet<DeliveryMethod> DeliveryMethods { set; get; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { set; get; }

    }
}
