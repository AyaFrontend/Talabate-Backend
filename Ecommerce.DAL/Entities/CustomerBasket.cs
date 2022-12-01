using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.DAL.Entities
{
    public class CustomerBasket
    {
        public string Id { set; get; }
        public IReadOnlyList<BasketItem> items { set; get; }
        public string PaymentIntentId { set; get; }
        public string ClientSecret { set; get; }
        public int? MethodDeliveryId { set; get; }
        public decimal ShippingCost { set; get; }
    }
}
