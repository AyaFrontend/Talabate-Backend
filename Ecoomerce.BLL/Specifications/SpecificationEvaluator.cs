using Ecommerce.DAL.Entities;
using Ecoomerce.BLL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecoomerce.BLL.Specifications
{
    public class SpecificationEvaluator<T> where T: BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery , ISpecfication<T> spec)
        {
            var query = inputQuery;

            if (spec.cretiria != null)
            query = inputQuery.Where(spec.cretiria);
       
                
            query = spec.includes.Aggregate(query, (currentQuery, include) => currentQuery.Include(include));

            if (spec.orderBy != null)
                query = query.OrderBy(spec.orderBy);

            if (spec.orderByDesc != null)
                query = query.OrderBy(spec.orderByDesc);

            if (spec.EnablePaginate)
               query=  query.Skip(spec.Skip).Take(spec.Take);

            return query;

           
        }
    }
}
