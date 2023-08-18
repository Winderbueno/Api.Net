using AutoMapper;
using Application.Dtos;
using Infrastructure.Identity.Dtos;

namespace Application.Mappers;

// https://docs.automapper.org/en/stable/Configuration.html#profile-instances
public class UserProfile : Profile
{
  public UserProfile()
  {
    CreateMap<IdentityDto, UserDto>().ReverseMap();
  }
}