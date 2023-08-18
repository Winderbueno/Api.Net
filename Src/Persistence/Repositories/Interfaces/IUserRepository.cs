using Domain.Entities;
using Domain.Enums;

namespace Persistence.Repositories.Interfaces;

public interface IUserRepository
{
  Task<UserK?> GetAsync(int id, bool deepLoad = false);
  Task<UserK?> GetByIdentityIdAsync(Guid identityId);
  Task<IEnumerable<UserK?>> SearchAsync(UserType[]? types, UserFunction[]? function);
}