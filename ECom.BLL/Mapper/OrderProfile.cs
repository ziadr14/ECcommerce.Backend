using AutoMapper;
using ECom.BLL.DTOs;
using ECom.DAL.Entities;
using ECom.DAL.Entities.Order;

namespace ECom.BLL.Mapping
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<BasketItem, OrderItem>();

            CreateMap<OrderItem, OrderItemDto>();


            CreateMap<ShippingAddressDto, ShippingAddress>();
            CreateMap<ShippingAddress, ShippingAddressDto>();

            CreateMap<Orders, OrderResponseDto>()
                .ForMember(d => d.DeliveryMethod,
                    o => o.MapFrom(s => s.DeliveryMethod.Name))
                .ForMember(d => d.DeliveryPrice,
                    o => o.MapFrom(s => s.DeliveryMethod.Price));
        }
    }
}
