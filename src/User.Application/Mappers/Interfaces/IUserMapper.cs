using User.Application.Dtos;
using User.Domain.Entities;
using User.Infrastructure.Identity.Dtos;

namespace User.Application.Mappers.Interfaces;

public interface IUserMapper
{
  UserDto ToUserDto(UserK user, IdentityDto identity);
  List<string> FlattenPermission(UserK user);
}