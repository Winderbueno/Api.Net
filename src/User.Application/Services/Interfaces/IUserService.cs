using User.Application.Dtos;

namespace User.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<int>> GetAsync();
        Task<UserDto> GetAsync(int id);
        Task<UserDto> CreateAsync(UserDto user);
        Task<UserDto> UpdateAsync(UserDto user);
    }
}
