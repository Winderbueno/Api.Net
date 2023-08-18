using Api.Controllers.Abstract;
using Application.Dtos;
using Application.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

// [ApiVersion("1.0")] - Todo. Api Versionning ?
[Produces("application/json")]
[AllowAnonymous] // Todo. This deactivate every Authorize attribute
public class UserController : BaseController
{
  private readonly IUserService _userService;
  private readonly IValidator<UserDto> _userValidator;

  public UserController(
      IValidator<UserDto> userValidator,
      IUserService userService)
  {
    _userValidator = userValidator;
    _userService = userService;
  }

  [HttpGet("{id:int}")]
  [Authorize("user.read")]
  public async Task<ActionResult<UserDto>> GetAsync(int id)
    => await _userService.GetAsync(id);

  [HttpPost("search")]
  [Authorize("user.read")]
  public async Task<ActionResult<IEnumerable<int>>> SearchAsync(UserSearchDto? dto)
      => Ok(await _userService.SearchAsync(dto));

  [HttpPost("create")]
  [Authorize("user.create")]
  public async Task<ActionResult<UserDto>> CreateAsync(UserDto user)
  {
    await _userValidator.ValidateAndThrowAsync(user);
    return await _userService.CreateAsync(user);
  }

  [HttpPut("update")]
  [Authorize("user.update")]
  public async Task<ActionResult<UserDto>> UpdateAsync(UserDto user)
  {
    await _userValidator.ValidateAndThrowAsync(user);
    return await _userService.UpdateAsync(user);
  }
}