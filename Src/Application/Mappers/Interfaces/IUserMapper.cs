using Application.Dtos;
using Domain.Entities;
using Infrastructure.Identity.Dtos;

namespace Application.Mappers.Interfaces;

public interface IUserMapper
{
  UserDto ToUserDto(UserK user, IdentityDto identity);
  List<string> FlattenPermission(UserK user);
}