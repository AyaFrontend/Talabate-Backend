using Ecommerce.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecoomerce.BLL.Interfaces
{
    public interface ISpecfication<T> where T: BaseEntity
    {
        public Expression<Func<T,bool>> cretiria { set; get; }
        public List<Expression<Func<T, object>>> includes { set; get; }
        public Expression<Func<T,object>> orderBy { set; get; }
        public Expression<Func<T, object>> orderByDesc { set; get; }
        public int Take { set; get; }
        public int Skip { set; get; }
        public bool EnablePaginate { set; get; }
    }
}
//context.Set<T>().where().include().include().orderby()