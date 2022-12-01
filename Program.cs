using Ecommerce.DAL.Data.Context;
using Ecommerce.DAL.Entities.Identites;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce
{
    public class Program
    {
       
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            var context = services.GetRequiredService<ApiDbContext>();
            var identityContext = services.GetRequiredService<IdentityContext>();
            var userManager = services.GetRequiredService<UserManager<AppUser>>();
            try
            {
               
                await context.Database.MigrateAsync();
                await identityContext.Database.MigrateAsync();
                await DataSeedContext.DataSeeding(loggerFactory, context);
                await IdentityContextDataSeed.SeedingUser(userManager);
            }
            catch(Exception ex)
            {
                
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex.Message);
            }
            
            await host.RunAsync();
   
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}