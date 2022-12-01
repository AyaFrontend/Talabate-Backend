using AutoMapper;
using Ecommerce.DAL.Entities;
using Ecommerce.DTO;
using Ecommerce.Errors;
using Ecoomerce.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
   
    public class CustomerBasketController : BaseController
    {
        private readonly IRedisRepository<CustomerBasket> _customerRepo;
        private readonly IMapper _mapper;


        public CustomerBasketController(IRedisRepository<CustomerBasket> customerRepo , IMapper mapper)
        {
            _customerRepo = customerRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> getBasket([FromQuery]string basketId)
        {
            var basket = await _customerRepo.GetAsync(basketId);
            if (basketId == null)
                return NotFound(new ApiResponse(StatusCodes.Status404NotFound));
            return Ok(basket);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> AddBasket(BasketCustomerDto basketDto)
        {
            var basket = _mapper.Map<BasketCustomerDto, CustomerBasket>(basketDto);
          
            var customerBasket = await _customerRepo.UpdateAsync(basket.Id, basket);
            if(customerBasket == null)
                return NotFound(new ApiResponse(StatusCodes.Status404NotFound));
            return Ok(customerBasket);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket(string basketId)
        {
            var deleted = await _customerRepo.DeleteAsync(basketId);
            if (!deleted)
                return NotFound(new ApiResponse(StatusCodes.Status404NotFound));
            return Ok(new ApiResponse(StatusCodes.Status200OK , "Basket is deleted"));
        }
    }
}
