using Ecommerce.DAL.Entities;
using Ecoomerce.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecoomerce.BLL.Repositories
{
    public class BaseSpecification<T> : ISpecfication<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> cretiria { get; set; }
        public List<Expression<Func<T, object>>> includes { get; set; } =
            new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> orderBy { get ; set; }
        public Expression<Func<T, object>> orderByDesc { get ; set ; }

        public int Take { get ; set ; }
        public int Skip { get ; set ; }
        public bool EnablePaginate { set; get; }



        public BaseSpecification(Expression<Func<T, bool>> cretiria)
        {
            this.cretiria = cretiria;
        
        }
        public BaseSpecification() { }

        public void AddInclude(Expression<Func<T, object>> include)
        {
            this.includes.Add(include);
        }

        public void AddOrderBy(Expression<Func<T, object>> orderBy)
        {
            this.orderBy = orderBy;
        }

        public void AddOrderByDesc(Expression<Func<T, object>> orderByDec)
        {
            this.orderByDesc = orderByDec;
        }

        public void ApplyingPagination(int take , int skip)
        {
            this.EnablePaginate = true;
            this.Skip = skip;
            this.Take = take;
        }
       
    }
}
