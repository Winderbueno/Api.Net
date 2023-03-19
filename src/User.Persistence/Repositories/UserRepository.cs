using User.Domain.Entities;
using User.Persistence.DbContexts;
using User.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace User.Persistence.Repositories
{
  public class UserRepository : IUserRepository
  {
    private readonly UserDbContext _userDb;

    public UserRepository(UserDbContext userDb)
      => _userDb = userDb;

    public async Task<IEnumerable<int>> Get()
      => await _userDb.Users.Select(u => u.UserId).ToListAsync();

    public async Task<UserK?> Get(int id, bool deepLoad = false)
    {
      UserK? user;

      if (deepLoad)
        user = await _userDb.Users
            .Include(user => user.Role!)
            .ThenInclude(role => role.Features!)
            .ThenInclude(feature => feature.Permissions)
            .SingleOrDefaultAsync(e => e.UserId == id);
      else
        user = _userDb.Users
            .SingleOrDefault(e => e.UserId == id);

      return user;
    }

    public async Task<UserK?> GetByIdentityId(Guid identityId)
      => await _userDb.Users
          .Include(user => user.Role!.Features!)
          .ThenInclude(feature => feature.Permissions)
          .SingleOrDefaultAsync(e => e.IdentityId == identityId);
  }
}