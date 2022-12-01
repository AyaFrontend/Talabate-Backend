using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ecoomerce.BLL.Services
{
    public class CacheResponseService : ICacheResponseService
    {

        private readonly IDatabase _redisDB;
        public CacheResponseService(IConnectionMultiplexer connection)
        {
            _redisDB = connection.GetDatabase();
        }


        public async Task CacheResponseAsync(string key, object response, TimeSpan timeSpan)
        {
            if (response == null) return ;

            var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var responseInJson =JsonSerializer.Serialize(response , options);

            await _redisDB.StringSetAsync(key, responseInJson, timeSpan);
        }

        public async Task<string> GetChachedResponse(string key)
        {
            return await _redisDB.StringGetAsync(key);
        }
    }
}
