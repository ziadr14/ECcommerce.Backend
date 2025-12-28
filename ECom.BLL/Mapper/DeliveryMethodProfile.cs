using AutoMapper;
using ECom.BLL.DTOs;
using ECom.DAL.Entities.Order;

public class DeliveryMethodProfile : Profile
{
    public DeliveryMethodProfile()
    {
        CreateMap<DeliveryMethod, DeliveryMethodDto>().ReverseMap();

        CreateMap<CreateDeliveryMethodDto, DeliveryMethod>();

        CreateMap<UpdateDeliveryMethodDto, DeliveryMethod>();
    }
}
