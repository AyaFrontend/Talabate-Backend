using Ecommerce.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecoomerce.BLL.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        public Task<T> GetByIdAsync(int id);
        public Task<IReadOnlyList<T>> GetAllAsync();
        public Task<int> AddAsync(T entity);
        public Task<int> UpdateAsync(T entity);
        public Task<int> DeleteAsync(T entity);

        public Task<IReadOnlyList<T>> GetAllSpec(ISpecfication<T> spec);
        public Task<int> GetCountAsync(ISpecfication<T> spec);
        public Task<T> GetByIdSpec(ISpecfication<T> spec);
    }
}
