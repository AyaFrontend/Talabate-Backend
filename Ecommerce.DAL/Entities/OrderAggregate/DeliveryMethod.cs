using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.DAL.Entities.OrderAggregate
{
    public class DeliveryMethod : BaseEntity
    {
        public string ShortName { set; get; }
        public string Description { set; get; }
        public string DeliveryTime { set; get; }
        public decimal Cost { set; get; } = 0;

        public DeliveryMethod()
        { }

        public DeliveryMethod(string shortName , string description , string deliveryTime , decimal cost)
        {
            ShortName = shortName;
            Description = description;
            DeliveryTime = deliveryTime;
            Cost = cost;

        }
    }
}
