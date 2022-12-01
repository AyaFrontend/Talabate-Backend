using Ecommerce.DAL.Entities;
using Ecommerce.DAL.Entities.OrderAggregate;
using Ecoomerce.BLL.Interfaces;
using Ecoomerce.BLL.Services;
using Ecoomerce.BLL.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
   
    public class PaymentsController : BaseController
    {
        private readonly IPaymentService _paymentService;
      

        public PaymentsController(IPaymentService paymentService )
        {
            _paymentService = paymentService;
         
        }

        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntentId(string basketId)
        {
            var basket = await _paymentService.CreateOrUpdatePaymentIntentAsync(basketId);
            if (basket == null) return null;

            return Ok(basket);
        }


        [HttpPost("webhook")]
        public async Task<ActionResult> StripeWebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], "1mwhsec_1407ca07eeb575052b07263953c6abb71b1fb8be05cc994e3c7c370944299b0");

                // Handle the event
                PaymentIntent intent = (PaymentIntent)stripeEvent.Data.Object;
                var order = await _paymentService.UpdatePaymentAsyn(intent.Id, stripeEvent);
               

                return Ok(order);
            }
            catch (StripeException e)
            {
                return BadRequest();
            }
        }
    }
}
