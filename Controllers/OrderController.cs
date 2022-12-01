using AutoMapper;
using Ecommerce.DAL.Entities.OrderAggregate;
using Ecommerce.DTO;
using Ecommerce.Errors;
using Ecoomerce.BLL.Interfaces;
using Ecoomerce.BLL.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        private IMapper _map;
        public OrderController(IOrderService orderService, IMapper map)
        {
            _orderService = orderService;
            _map = map;
        }

       
        [HttpPost("createOrder")]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto order)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var addressToShip = _map.Map<AddressDto, Address>(order.addressToship);
           var result = await _orderService.CreateOrder(order.deliveryMethodId, addressToShip, order.BasketId, userEmail);
            if (result == null)
                return BadRequest(new ApiValidationResponse() { errors = new[] { "An error occured during create order please try again" } });
            return Ok(result);
        }


        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderReturnDto>>> GetOrder()
        {
            var userEmail =  User.FindFirstValue(ClaimTypes.Email);
            var orders = await _orderService.GetAllOrdersByUserEmailAsync(userEmail);
            if (orders.Count == 0)
                return NotFound(new ApiResponse(StatusCodes.Status404NotFound));

            return Ok(_map.Map<IReadOnlyList<Order>, IReadOnlyList<OrderReturnDto>>(orders) );
        
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<OrderReturnDto>> GetOrder(int id)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var order = await _orderService.GetOrderByUserEmailAsync(id,userEmail);
            if (order == null)
                return NotFound(new ApiResponse(StatusCodes.Status404NotFound));

            return Ok(_map.Map<Order, OrderReturnDto>(order));

        }


        [HttpGet("DeliveryMethods")]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetAllDeliveryMethods()
        {
           
            var deliveryMethods = await _orderService.GetAllDeliveryMethodsAsync();
            if (deliveryMethods.Count == 0)
                return NotFound(new ApiResponse(StatusCodes.Status404NotFound));

            return Ok(deliveryMethods);

        }
    }
}
