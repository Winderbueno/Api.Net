using User.Domain.Enums;

namespace User.Application.Dtos;

public class UserSearchDto
{
    public string? Contains { get; set; }
    public UserStatus?[]? Status { get; set; }
    public UserType[]? Types { get; set; }
    public UserFunction[]? Functions { get; set; }
}