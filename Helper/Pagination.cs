using Ecommerce.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Helper
{
    public class Pagination<T>
    {
        public int PageIndex { set; get; }
        public int Count { set; get; }
        public int PageSize { set; get; }
        public IReadOnlyList<T> Data { set; get; } //= new HashSet<T>();

        public Pagination(IReadOnlyList<T> data, int pageIndex, int pageSize , int count)
        {
            Data = data;
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
        }

   

    
    }
}
