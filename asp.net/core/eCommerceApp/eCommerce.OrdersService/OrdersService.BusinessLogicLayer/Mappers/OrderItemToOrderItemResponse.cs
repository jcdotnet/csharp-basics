using AutoMapper;
using OrdersService.BusinessLogicLayer.DTO;
using OrdersService.DataAccessLayer.Entities;

namespace OrdersService.BusinessLogicLayer.Mappers
{
    public class OrderItemToOrderItemResponse : Profile
    {
        public OrderItemToOrderItemResponse()
        {
            CreateMap<OrderItem, OrderItemResponse>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice));
        }
    }
}
