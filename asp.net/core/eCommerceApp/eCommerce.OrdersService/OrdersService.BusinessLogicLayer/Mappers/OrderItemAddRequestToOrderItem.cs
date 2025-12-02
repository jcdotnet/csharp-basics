using AutoMapper;
using OrdersService.BusinessLogicLayer.DTO;
using OrdersService.DataAccessLayer.Entities;

namespace OrdersService.BusinessLogicLayer.Mappers
{
    public class OrderItemAddRequestToOrderItem : Profile
    {
        public OrderItemAddRequestToOrderItem()
        {
            CreateMap<OrderItemAddRequest, OrderItem>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.TotalPrice, opt => opt.Ignore())
            .ForMember(dest => dest._id, opt => opt.Ignore());
        }
    }
}
