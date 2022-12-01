using AutoMapper;
using Ecommerce.DAL.Entities;
using Ecommerce.DAL.Entities.Identites;
using Ecommerce.DAL.Entities.OrderAggregate;
using Ecommerce.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>().ForMember(d => d.BrandName, o => o.MapFrom(p => p.ProductBrand.Name)).
                ForMember(d => d.TypeName, o => o.MapFrom(m => m.ProductType.Name)).
                ForMember(d => d.ImageFullPath, o => o.MapFrom<PictureUrlResolver>());


            CreateMap<RegisterDto, AppUser>().ForMember(o=>o.Address , m => m.MapFrom<AddressResolver>());
            CreateMap<Ecommerce.DAL.Entities.OrderAggregate.Address, AddressDto>().ReverseMap();
            CreateMap<Ecommerce.DAL.Entities.Identites.Address, AddressDto>().ReverseMap();
            CreateMap<CustomerBasket,BasketCustomerDto>().ReverseMap();
            CreateMap<BasketItemDto, BasketItem>().ReverseMap();

            CreateMap<Order, OrderReturnDto>().ForMember(o => o.deliveryMethodName, o => o.MapFrom(S => S.deliveryMethod.ShortName)).
                ForMember(o => o.deliveryMethodCost, S => S.MapFrom(S => S.deliveryMethod.Cost));

            CreateMap<OrderItem, OrderItemReturnDto>().ForMember(o => o.ProductId,
                S => S.MapFrom(S => S.ProductOrderedItem.ProductId)).
                ForMember(o => o.ProductName,
                S => S.MapFrom(S => S.ProductOrderedItem.ProductName))
                .ForMember(o => o.UrlPic,
                S => S.MapFrom(S => S.ProductOrderedItem.UrlPic));
        }
    }
}
