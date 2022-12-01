using Ecommerce.DAL.Data.Context;
using Ecommerce.DAL.Entities;
using Ecoomerce.BLL.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Ecoomerce.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable repositories;

        private readonly ApiDbContext _context;
        public UnitOfWork(ApiDbContext context)
        { _context = context; }


        public void Dispose()
        {
           // throw new NotImplementedException();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (repositories == null)
                repositories = new Hashtable();
            var key = typeof(TEntity).Name;
            if(!repositories.ContainsKey(key))
            {
                 repositories.Add(key, new GenericRepository<TEntity>(_context));
            }
            return (IGenericRepository<TEntity>)repositories[key];
        }
    }
}
