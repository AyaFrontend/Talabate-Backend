using Ecommerce.DAL.Entities.OrderAggregate;
using Ecoomerce.BLL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecoomerce.BLL.Specifications
{
    public class OrderSpecificationWithDeliveryMethodAndItems : BaseSpecification<Order>
    {
        public OrderSpecificationWithDeliveryMethodAndItems(string userEmail):base(o=>o.UserEmail == userEmail)
        {
            AddInclude(o => o.deliveryMethod);
            AddInclude(o => o.orderItems);
            AddOrderByDesc(o => o.TimeOffset);
        }

        public OrderSpecificationWithDeliveryMethodAndItems(int orderId ,string userEmail) : base(o => o.UserEmail == userEmail && o.Id == orderId)
        {
            AddInclude(o => o.deliveryMethod);
            AddInclude(o => o.orderItems);
            AddOrderByDesc(o => o.TimeOffset);
        }
    }
}


