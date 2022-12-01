using Castle.Core.Logging;
using Ecommerce.Errors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ecommerce.Middlwares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment env;

        public ExceptionMiddleware(RequestDelegate next , ILogger<ExceptionMiddleware> logger , IHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }

        public async Task InvokeAsync (HttpContext context)
        {
            try
            {
               await next(context);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, ex.Message);
              
                context.Response.ContentType = "Application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = env.IsDevelopment() ? new ApiExceptionResponse((int)HttpStatusCode.InternalServerError , ex.Message,  ex.StackTrace.ToString()                                                                              
                          ) : new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);
                var option = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                
                var jsonResponse = JsonSerializer.Serialize(response , option);
                await context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}
