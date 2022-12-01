using Ecommerce.DAL.Data.Context;
using Ecommerce.DAL.Entities;
using Ecoomerce.BLL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ecoomerce.BLL.Specifications;

namespace Ecoomerce.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {

        private readonly ApiDbContext _context;
        public GenericRepository(ApiDbContext context)
        {
            _context = context;
        }


        public async Task<int> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(T entity)
        {
           // var entity = await _context.Set<T>().Where(e => e.Id == id).FirstOrDefaultAsync();
            _context.Set<T>().Remove(entity);
            return await _context.SaveChangesAsync();

           
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllSpec(ISpecfication<T> spec)
        {

            return await this.ApplyingSpecification(spec).ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().Where(e => e.Id == id).FirstOrDefaultAsync();
                
        }

        public async Task<T> GetByIdSpec( ISpecfication<T> spec)
        {
            return await this.ApplyingSpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<int> GetCountAsync(ISpecfication<T> spec)
        {
            return await ApplyingSpecification(spec).CountAsync();
        }

        public async Task<int> UpdateAsync( T entity)
        {
            _context.Set<T>().Update(entity);
            return await _context.SaveChangesAsync();
        }


        private IQueryable<T> ApplyingSpecification(ISpecfication<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), spec);
        }
    }
}
