using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecoomerce.BLL.Services
{
    public interface ICacheResponseService
    {
        public Task CacheResponseAsync(string key, object response, TimeSpan timeSpan);
        public Task<string> GetChachedResponse(string key);
    }
}
