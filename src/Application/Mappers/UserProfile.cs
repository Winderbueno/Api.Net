using Application.Dtos;
using AutoMapper;
using User.Infrastructure.Identity.Dto;

namespace Application.Mappers
{
    // https://docs.automapper.org/en/stable/Configuration.html#profile-instances
    public class UserProfile : Profile
	{
		public UserProfile()
		{
			CreateMap<Identity, UserDto>().ReverseMap();
		}
	}
}
