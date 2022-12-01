using AutoMapper;
using Ecommerce.DAL.Entities.Identites;
using Ecommerce.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Helper
{
    public class AddressResolver : IValueResolver<RegisterDto, AppUser, Address>
    {
        public Address Resolve(RegisterDto source, AppUser destination, Address destMember, ResolutionContext context)
        {
            return destination.Address = new Address()
            {
                Street = source.Street,
                Country = source.Country,
                City = source.City
            };
        }
    }
}
