
using Ecommerce.DAL.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.DTO
{
    public class OrderReturnDto
    {
        public string UserEmail { set; get; }
        public string orderStatus { set; get; } 
        public Address AddressToShip { set; get; }
        public string  deliveryMethodName { set; get; }
        public decimal deliveryMethodCost { set; get; }
        public List<OrderItemReturnDto> orderItems { set; get; } 
        public DateTimeOffset TimeOffset { set; get; } 
        public string paymentIntentId { set; get; }
        public decimal SubTotal { set; get; }
        public decimal Total { set; get; }
    }
}
