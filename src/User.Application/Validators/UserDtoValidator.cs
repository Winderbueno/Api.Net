using FluentValidation;
using User.Application.Dtos;

namespace User.Application.Validators
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(x => x.Mail).EmailAddress();
        }
    }
}
