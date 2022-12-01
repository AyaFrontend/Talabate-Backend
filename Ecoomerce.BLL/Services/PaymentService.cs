using Ecommerce.DAL.Entities;
using Ecommerce.DAL.Entities.OrderAggregate;
using Ecoomerce.BLL.Interfaces;
using Ecoomerce.BLL.Repositories;
using Ecoomerce.BLL.Specifications;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = Ecommerce.DAL.Entities.Product;
namespace Ecoomerce.BLL.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _unit;
        private readonly IRedisRepository<CustomerBasket> _Repo;


        public PaymentService(IConfiguration config, IUnitOfWork unit,
            IRedisRepository<CustomerBasket> Repo)
        {
            _config = config;
            _unit = unit;
            _Repo = Repo;
        }

        public async Task<CustomerBasket> CreateOrUpdatePaymentIntentAsync(string basketId)
        {
            StripeConfiguration.ApiKey = _config["PaymentSetting:Secretkey"];

            var basket = await _Repo.GetAsync(basketId);



            if (basket == null) return null;

            var shippingPrice = 0m;

            if (basket.MethodDeliveryId.HasValue)
            {
                var deliveryMethod = await _unit.Repository<DeliveryMethod>().GetByIdAsync(basket.MethodDeliveryId.Value);
                shippingPrice = deliveryMethod.Cost;
            }

            foreach (var item in basket.items)
            {
                var productItem = await _unit.Repository<Product>().GetByIdAsync(item.Id);
                if (item.Price != productItem.Price)
                    item.Price = productItem.Price;
            }

            var service = new PaymentIntentService();

            PaymentIntent intent;

            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)basket.items.Sum(i => i.Quantity * (i.Price * 100)) + ((long)shippingPrice * 100),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                intent = await service.CreateAsync(options);
                basket.PaymentIntentId = intent.Id;
                basket.ClientSecret = intent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)basket.items.Sum(i => (i.Quantity * (i.Price * 100))) + (long)(shippingPrice * 100)
                };

                await service.UpdateAsync(basket.PaymentIntentId, options);

            }
            basket.ShippingCost = shippingPrice;
            await _Repo.UpdateAsync(basketId, basket);

            return basket;
        }

        public async Task<Order> UpdatePaymentAsyn(string paymntIntentId, Event stripeEvent)
        {
            var spec = new OrderwithPaymentIntentIdSpec(paymntIntentId);
            var order = await _unit.Repository<Order>().GetByIdSpec(spec);
            if (order == null) return null;
            switch (stripeEvent.Type)
            {
                case Events.PaymentIntentSucceeded:
                    order.orderStatus = OrderStatus.OrderRecived;
                    break;
                case Events.PaymentIntentPaymentFailed:
                    order.orderStatus = OrderStatus.OrderFailed;
                    break;
            }


            await _unit.Repository<Order>().UpdateAsync(order);
            return order;
            
        }
    }
}
