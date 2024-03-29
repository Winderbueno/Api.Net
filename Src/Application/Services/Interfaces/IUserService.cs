﻿using Application.Dtos;

namespace Application.Services.Interfaces;

public interface IUserService
{
  Task<UserDto> GetAsync(int id);
  Task<IEnumerable<int>> SearchAsync(UserSearchDto? dto);
  Task<UserDto> CreateAsync(UserDto dto);
  Task<UserDto> UpdateAsync(UserDto dto);
}