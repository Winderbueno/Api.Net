using AutoMapper;
using User.Application.Dtos;
using User.Infrastructure.Identity.Dtos;

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