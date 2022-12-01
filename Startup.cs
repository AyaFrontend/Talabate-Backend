using Ecommerce.DAL.Data.Context;
using Ecommerce.DAL.Entities;
using Ecoomerce.BLL.Interfaces;
using Ecoomerce.BLL.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Proxies;
using Ecommerce.Helper;
using Ecommerce.Errors;
using Ecommerce.Middlwares;
using StackExchange.Redis;
using Ecommerce.DAL.Entities.Identites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Ecoomerce.BLL.Services;

namespace Ecommerce
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ecommerce", Version = "v1" });
            });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddDbContext<ApiDbContext>(options =>
            options./*UseLazyLoadingProxies().*/UseSqlServer(Configuration.GetConnectionString("DbConnectionString")));
      
            services.Configure<ApiBehaviorOptions>(o => o.InvalidModelStateResponseFactory =
            actionContext => 
            {
               var Errors =   actionContext.ModelState.Where(e => e.Value.Errors.Count > 0).
                              SelectMany(e => e.Value.Errors).Select(e => e.ErrorMessage).ToList();
               var validationResponse = new ApiValidationResponse()
               { errors = Errors };
               return new BadRequestObjectResult(validationResponse);
            });

            services.AddDbContext<IdentityContext>(options => options.UseSqlServer(Configuration.GetConnectionString("IdentityDB")));

            services.AddSingleton<IConnectionMultiplexer>(c =>
            {
                var connect =ConfigurationOptions.Parse( Configuration.GetConnectionString("redis"));
                return ConnectionMultiplexer.Connect(connect);
            });

            services.AddScoped(typeof(ITokenService), typeof(TokenService));
            services.AddScoped(typeof(IPaymentService), typeof(PaymentService));
            services.AddScoped(typeof(IRedisRepository<>), typeof(RedisRepository<>));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

            services.AddSingleton(typeof(ICacheResponseService),typeof(CacheResponseService));
            services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<IdentityContext>();
        
            
            services.AddAuthentication(option => {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                
                }  ).AddJwtBearer(options =>
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
            {
                ValidateAudience = true,
                ValidAudience = Configuration["JWT:validAudience"],
                ValidateIssuer = true,
                ValidIssuer = Configuration["JWT:validIssuer"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:key"])),
                ValidateLifetime =true
            }) ;

            services.AddScoped<IOrderService, OrderService>();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy" , policy =>
                {
                    policy.AllowAnyHeader().AllowAnyOrigin();
                });
            });
        }
       
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           
            if (env.IsDevelopment())
            {
                app.UseMiddleware<ExceptionMiddleware>();
                //app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ecommerce v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
         
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
