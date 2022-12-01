using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.DAL.Entities.OrderAggregate
{
    public class ProductItemOrdered
    {

        public int ProductId { set; get; }
        public string UrlPic { set; get; }
        public string ProductName { set; get; }

        public ProductItemOrdered()
        { }
        
        public ProductItemOrdered(int productId , string urlPic , string productName)
        {
            ProductId = productId;
            UrlPic = urlPic;
            ProductName = productName;
        }
       
    }
}
