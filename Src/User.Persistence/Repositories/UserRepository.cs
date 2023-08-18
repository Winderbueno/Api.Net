using Microsoft.EntityFrameworkCore;
using User.Domain.Entities;
using User.Domain.Enums;
using User.Persistence.Db;
using User.Persistence.Repositories.Interfaces;

namespace User.Persistence.Repositories;

public class UserRepository : IUserRepository
{
  private readonly UserDbContext _userDb;

  public UserRepository(UserDbContext userDb)
    => _userDb = userDb;
  
  public async Task<UserK?> GetAsync(int id, bool deepLoad = false)
  {
    UserK? user;

    if (deepLoad)
      user = await _userDb.Users
          .Include(user => user.Role!)
          .ThenInclude(role => role.Features!)
          .ThenInclude(feature => feature.Permissions)
          .SingleOrDefaultAsync(e => e.UserKId == id);
    else
      user = _userDb.Users
          .SingleOrDefault(e => e.UserKId == id);

    return user;
  }

  public async Task<UserK?> GetByIdentityIdAsync(Guid identityId)
    => await _userDb.Users
        .Include(user => user.Role!.Features!)
        .ThenInclude(feature => feature.Permissions)
        .SingleOrDefaultAsync(e => e.IdentityId == identityId);

  public async Task<IEnumerable<UserK?>> SearchAsync(
        UserType[]? types, 
        UserFunction[]? functions)
    {
        var users = _userDb.Users.Include(user => user.Role).AsQueryable();

        users = (types != null && types.Any()) ? users.Where(u => types.Contains(u.Role!.UserType)) : users;
        users = (functions != null && functions.Any()) ? users.Where(u => functions.Contains(u.Role!.Function)) : users;

        return await users.ToListAsync();
    }
}