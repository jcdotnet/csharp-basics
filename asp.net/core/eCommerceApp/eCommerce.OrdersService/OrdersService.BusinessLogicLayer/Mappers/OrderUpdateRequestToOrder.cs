using AutoMapper;
using OrdersService.BusinessLogicLayer.DTO;
using OrdersService.DataAccessLayer.Entities;

namespace OrdersService.BusinessLogicLayer.Mappers
{
    public class OrderUpdateRequestToOrder : Profile
    {
        public OrderUpdateRequestToOrder()
        {
            CreateMap<OrderUpdateRequest, Order>()
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))      
            .ForMember(dest => dest._id, opt => opt.Ignore())
            .ForMember(dest => dest.TotalAmount, opt => opt.Ignore());
        }
    }
}
