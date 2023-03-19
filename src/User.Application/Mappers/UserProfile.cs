using User.Application.Dtos;
using AutoMapper;
using User.Infrastructure.Identity.Dto;

namespace User.Application.Mappers
{
    // https://docs.automapper.org/en/stable/Configuration.html#profile-instances
    public class UserProfile : Profile
	{
		public UserProfile()
		{
			CreateMap<IdentityDto, UserDto>().ReverseMap();
		}
	}
}