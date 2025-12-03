
using AutoMapper;
using OrdersService.BusinessLogicLayer.DTO;

namespace OrdersService.BusinessLogicLayer.Mappers
{
    public class ProductDtoToOrderItemResponse : Profile
    {
        public ProductDtoToOrderItemResponse()
        {
            CreateMap<ProductDto, OrderItemResponse>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));
        }     
    }
}
