using System;
using System.Collections.Generic;
using System.Text;

namespace Ecoomerce.BLL.Specifications
{
    public class ProductSpecificationParameters
    {
       public string order { set; get; }
       public int? brandId { set; get; }
       public int? typeId { set; get; }
       public int PageIndex { set; get; } = 1;
       private int pageSize = 5;
       private string name;

       public string Name
        {
            get { return name; }
            set { name = value.ToLower(); }
        }

       public int PageSize
       {
            get { return pageSize; }
            set { pageSize = pageSize > 5 ? 5 : value; }
       }
       

    }
}
