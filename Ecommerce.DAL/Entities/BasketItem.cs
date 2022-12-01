using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.DAL.Entities
{
    public class BasketItem : BaseEntity
    {
        public string ProductName { set; get; }
        public string BrandName { set; get; }
        public string TypeName { set; get; }
        public decimal Price { set; get; }
        public string PicUrl { set; get; }
        public int Quantity { set; get; }
    }
}
