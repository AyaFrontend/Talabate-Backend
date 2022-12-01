using Ecoomerce.BLL.Interfaces;
using Ecoomerce.BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Helper
{
    public class CacheResponse : Attribute, IAsyncActionFilter
    {

        private int _timeSpan { set; get; }
        public CacheResponse(int timeSpan)
        {
            _timeSpan = timeSpan;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = (ICacheResponseService)context.HttpContext.RequestServices.GetService(typeof(ICacheResponseService));

            var key = generateCachedKey(context.HttpContext.Request);
            var response = await cacheService.GetChachedResponse(key);
            if(!string.IsNullOrEmpty(response))
            {
                var contentResult = new ContentResult()
                {
                    Content = response,
                    StatusCode = 200,
                    ContentType = "application/json"

                };
                context.Result = contentResult;
                return;
            }

            var endPointResponse = await next.Invoke();
            if(endPointResponse.Result is OkObjectResult )
            {
                await cacheService.CacheResponseAsync(key, endPointResponse.Result, TimeSpan.FromSeconds(_timeSpan));
            }
        }

        private string generateCachedKey(HttpRequest request)
        {
            var cachedKey = new StringBuilder();
            cachedKey.Append($"{request.Path}");

            foreach (var (Key,Value)  in request.Query )
            {
                cachedKey.Append($"{Key}-{Value} |");
            }

            return cachedKey.ToString();
        }
    }
}
