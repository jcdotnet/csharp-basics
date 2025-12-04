using AutoMapper;

namespace OrdersService.BusinessLogicLayer.DTO
{
    public class UserDtoToOrderResponse : Profile
    {
        public UserDtoToOrderResponse()
        {
            CreateMap<UserDto, OrderResponse>()
            .ForMember(dest => dest.UserDisplayName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
        }
    }
}
