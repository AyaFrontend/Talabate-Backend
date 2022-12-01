using Ecommerce.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecoomerce.BLL.Interfaces
{
    public interface IUnitOfWork :IDisposable
    {
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        
    }
}
