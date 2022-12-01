using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.DAL.Entities.OrderAggregate
{
    public class OrderItem : BaseEntity
    {
        public decimal Price { set; get; }
        public int Quantatiy { set; get; }
        public ProductItemOrdered ProductOrderedItem { set; get; }

        public OrderItem()
        {

        }

        public OrderItem(decimal price , int quantity , ProductItemOrdered productItemOrdered)
        {
            Price = price;
            Quantatiy = quantity;
            this.ProductOrderedItem = productItemOrdered;
            
        }
    }
}
