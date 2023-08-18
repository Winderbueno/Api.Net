using Application.Dtos;
using FluentValidation;

namespace Application.Validators;

public class UserDtoValidator : AbstractValidator<UserDto>
{
  public UserDtoValidator()
  {
    RuleFor(x => x.Mail).EmailAddress();
  }
}