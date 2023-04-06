using User.Api.Controllers.Abstract;
using User.Application.Dtos;
using User.Application.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace User.Api.Controllers
{
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
        /// Read
        /// </summary>
        [HttpGet()]
        //[Authorize("user.read")]
        [AllowAnonymous]
        public async Task<ActionResult<int[]>> GetAsync()
          => (await _userService.GetAsync()).ToArray();

        /// <summary>
        /// Read by id
        /// </summary>
        [HttpGet("{id:int}")]
        [Authorize("user.read")]
        public async Task<ActionResult<UserDto>> GetAsync(int id)
          => await _userService.GetAsync(id);

        /// <summary>
        /// Create
        /// </summary>
        [HttpPost()]
        [Authorize("user.create")]
        public async Task<ActionResult<UserDto>> CreateAsync(UserDto user)
        {
            await _userValidator.ValidateAndThrowAsync(user);            
            
            return await _userService.CreateAsync(user);
        }

        /// <summary>
        /// Update
        /// </summary>
        [HttpPut()]
        [Authorize("user.update")]
        public async Task<ActionResult<UserDto>> UpdateAsync(UserDto user)
        {
            await _userValidator.ValidateAndThrowAsync(user);

            return await _userService.UpdateAsync(user);
        }
    }
}
