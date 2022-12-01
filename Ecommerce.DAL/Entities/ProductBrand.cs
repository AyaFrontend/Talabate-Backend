using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.DAL.Entities
{
    public class ProductBrand : BaseEntity
    {
        public string Name { set; get; }
      
        public /*virtual*/  ICollection<Product> Products { set; get; } =
            new HashSet<Product>();
    }
}
