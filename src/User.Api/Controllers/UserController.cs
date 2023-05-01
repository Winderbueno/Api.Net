using User.Api.Controllers.Abstract;
using User.Application.Dtos;
using User.Application.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace User.Api.Controllers;

// [ApiVersion("1.0")]
[Produces("application/json")]
public class UserController : BaseController
{
  private readonly IUserService _userService;
  private readonly IValidator<UserDto> _userValidator;

  public UserController(
      IValidator<UserDto> userValidator,
      IUserService userService)
  {
    this._userValidator = userValidator;
    this._userService = userService;
  }

  /// <summary>
  /// Read by id
  /// </summary>
  [HttpGet("{id:int}")]
  //[Authorize("user.read")]
  [AllowAnonymous]
  public async Task<ActionResult<UserDto>> GetAsync(int id)
    => await _userService.GetAsync(id);

  /// <summary>
  /// Search
  /// </summary>
  [HttpPost("search")]
  [Authorize("user.read")]
  public async Task<ActionResult<IEnumerable<int>>> SearchAsync(UserSearchDto? dto)
      => Ok(await _userService.SearchAsync(dto));

  /// <summary>
  /// Create
  /// </summary>
  [HttpPost("create")]
  [Authorize("user.create")]
  public async Task<ActionResult<UserDto>> CreateAsync(UserDto user)
  {
    await _userValidator.ValidateAndThrowAsync(user);

    return await _userService.CreateAsync(user);
  }

  /// <summary>
  /// Update
  /// </summary>
  [HttpPut("update")]
  [Authorize("user.update")]
  public async Task<ActionResult<UserDto>> UpdateAsync(UserDto user)
  {
    await _userValidator.ValidateAndThrowAsync(user);

    return await _userService.UpdateAsync(user);
  }
}