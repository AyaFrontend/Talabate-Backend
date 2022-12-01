using AutoMapper;

using Ecommerce.DAL.Entities.OrderAggregate;
using Ecommerce.DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Helper
{
    public class OrderPicResolver : IValueResolver<OrderItemReturnDto, OrderItemReturnDto, string>
    {
        private IConfiguration _config;
        public OrderPicResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(OrderItemReturnDto source, OrderItemReturnDto destination, string destMember, ResolutionContext context)
        {
            return $"{_config["baseUrl"]}{source.UrlPic}";
        }
    }
}
