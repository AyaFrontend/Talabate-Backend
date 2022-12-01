using Ecommerce.DAL.Entities.OrderAggregate;
using Ecoomerce.BLL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecoomerce.BLL.Specifications
{
    public class OrderwithPaymentIntentIdSpec : BaseSpecification<Order>
    {
        public OrderwithPaymentIntentIdSpec(string PaymentIntentId):base(O => O.PaymentIntentId == PaymentIntentId)
        { }
    }
}
