using AutoMapper;
using OrdersService.BusinessLogicLayer.DTO;
using OrdersService.DataAccessLayer.Entities;

namespace OrdersService.BusinessLogicLayer.Mappers
{
    public class OrderAddRequestToOrder : Profile
    {
        public OrderAddRequestToOrder()
        {
            CreateMap<OrderAddRequest, Order>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
            .ForMember(dest => dest.OrderId, opt => opt.Ignore())
            .ForMember(dest => dest._id, opt => opt.Ignore())
            .ForMember(dest => dest.TotalAmount, opt => opt.Ignore());
        }
    }
}
