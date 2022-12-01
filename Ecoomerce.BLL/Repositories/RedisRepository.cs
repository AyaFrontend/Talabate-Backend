using Ecoomerce.BLL.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ecoomerce.BLL.Repositories
{
    public class RedisRepository<T> : IRedisRepository<T> where T :class
    {
        private readonly IDatabase _redisDatabase;

        public RedisRepository(IConnectionMultiplexer connection)
        {
            _redisDatabase = connection.GetDatabase() ;
        }


        public async Task<bool> DeleteAsync(string key)
        {
           return await _redisDatabase.KeyDeleteAsync(key);
        }

        public async Task<T> GetAsync(string key)
        {
            var item = await _redisDatabase.StringGetAsync(key);
            return string.IsNullOrWhiteSpace(item) ? null : JsonSerializer.Deserialize<T>(item);
        }

        public async Task<T> UpdateAsync(string key ,T values)
        {
            var created = await _redisDatabase.StringSetAsync(key, JsonSerializer.Serialize(values), TimeSpan.FromDays(2));
            if (!created)
                return null;
            return await this.GetAsync(key);
        }
    }
}
