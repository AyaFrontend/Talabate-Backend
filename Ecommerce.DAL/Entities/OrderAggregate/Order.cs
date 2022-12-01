using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.DAL.Entities.OrderAggregate
{
    public class Order:BaseEntity
    {
        public string UserEmail { set; get; }
        public OrderStatus orderStatus { set; get; } = OrderStatus.Pending;
        public Address AddressToShip { set; get; }
        public DeliveryMethod deliveryMethod { set; get; }
        public List<OrderItem> orderItems { set; get; } = new List<OrderItem>();
        public DateTimeOffset TimeOffset { set; get; } = DateTimeOffset.Now;
        public string PaymentIntentId { set; get; }
        public decimal SubTotal { set; get; }


        public Order()
        { }
        public Order(string userEmail , Address addressToShip, DeliveryMethod delivery , List<OrderItem> items, decimal subTotal , string paymentIntentId)
        {
            UserEmail = userEmail;
            AddressToShip = addressToShip;
            deliveryMethod = delivery;
            orderItems = items;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
        }

        public decimal getTotal => deliveryMethod.Cost + SubTotal;
    }


}
