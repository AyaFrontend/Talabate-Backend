using Ecommerce.DAL.Entities;
using Ecommerce.DAL.Entities.OrderAggregate;
using Stripe;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecoomerce.BLL.Services
{
    public interface IPaymentService
    {
        public Task<CustomerBasket> CreateOrUpdatePaymentIntentAsync(string basketId);
        // public Task<Order> UpdatePaymentSucceedAsync(string paymntIntentId);
        // public Task<Order> UpdatePaymentFailedAsyn(string paymntIntentId);
        public Task<Order> UpdatePaymentAsyn(string paymntIntentId , Event stripeEvent);

    }
}
