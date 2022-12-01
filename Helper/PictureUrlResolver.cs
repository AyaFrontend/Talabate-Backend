using AutoMapper;

using Ecommerce.DAL.Entities;
using Ecommerce.DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Helper
{
    public class PictureUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration _config;

        public PictureUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                return $"{_config["baseUrl"]}{source.PictureUrl}";

            return null;
        }
    }
}
