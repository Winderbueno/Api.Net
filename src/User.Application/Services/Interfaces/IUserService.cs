using User.Application.Dtos;

namespace User.Application.Services.Interfaces;

public interface IUserService
{
  Task<UserDto> GetAsync(int id);
  Task<IEnumerable<int>> SearchAsync(UserSearchDto? dto);
  Task<UserDto> CreateAsync(UserDto dto);
  Task<UserDto> UpdateAsync(UserDto dto);
}