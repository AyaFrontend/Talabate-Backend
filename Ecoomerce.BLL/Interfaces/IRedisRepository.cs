using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecoomerce.BLL.Interfaces
{
    public interface IRedisRepository<T>
    {
        public Task<T> GetAsync(string Key);
        public Task<T> UpdateAsync( string key, T values);
        public Task<bool> DeleteAsync(string key);
    }
}
