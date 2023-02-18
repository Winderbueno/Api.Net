using Application.Dtos;

namespace Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<int>> GetAsync();
        Task<UserDto> GetAsync(int id);
        Task<UserDto> CreateAsync(UserDto user);
        Task<UserDto> UpdateAsync(UserDto user);
    }
}
