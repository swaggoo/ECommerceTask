using AutoMapper;
using ECommerce.Core.DTOs;
using ECommerce.Core.Entities;

namespace ECommerce.Core.MappingsRegistration;

public class ECommerceMappings : Profile
{
    public ECommerceMappings()
    {
        CreateMap<OrderProduct, OrderTableModel>()
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Order.CreatedOn.ToString("yyyy-MM-dd")))
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Order.OrderStatus))
            .ForMember(dest => dest.CustomerFullname, opt => opt.MapFrom(src => src.Order.CustomerFullName))
            .ForMember(dest => dest.CustomerPhone, opt => opt.MapFrom(src => src.Order.CustomerPhone));
        
        CreateMap<ProductDto, Product>().ReverseMap();
        CreateMap<OrderDto, Order>().ReverseMap();
    }
}