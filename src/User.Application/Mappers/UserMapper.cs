using AutoMapper;
using User.Application.Dtos;
using User.Application.Mappers.Interfaces;
using User.Domain.Entities;
using User.Infrastructure.Identity.Dtos;

namespace User.Application.Services;

public class UserMapper : IUserMapper
{
  private readonly IMapper _mapper;

  public UserMapper(IMapper mapper) => _mapper = mapper;

  public UserDto ToUserDto(UserK user, IdentityDto identityDto)
  {
    // Map Identity
    UserDto userDto = _mapper.Map<UserDto>(identityDto);

    // Map User
    if (userDto != null)
    {
      userDto.UserId = user.UserId;
      userDto.RoleId = user.RoleId;
      userDto.Permissions = FlattenPermission(user);
    }

    return userDto!;
  }

  public List<string> FlattenPermission(UserK user)
  {
    return user.Role?.Features?.ToList()
      .Aggregate(
        new List<string>(),
        (result, value) =>
        {
          result.AddRange(value.Permissions!.Select(p => p.Name)!);
          return result;
        }) ?? new List<string>();
  }
}