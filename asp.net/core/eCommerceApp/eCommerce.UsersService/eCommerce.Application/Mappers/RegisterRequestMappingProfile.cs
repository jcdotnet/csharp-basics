using AutoMapper;
using eCommerce.Application.DTO;
using eCommerce.Domain.Entities;

namespace eCommerce.Application.Mappers
{

    public class RegisterRequestMappingProfile : Profile
    {
        public RegisterRequestMappingProfile()
        {
            CreateMap<RegisterRequest, ApplicationUser>()
                .ForMember(dest => dest.Email, options => options.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, options => options.MapFrom(src => src.Password))
                .ForMember(dest => dest.UserName, options => options.MapFrom(src => src.UserName));         
        }
    }
}
