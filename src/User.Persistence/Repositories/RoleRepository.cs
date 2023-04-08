using User.Domain.Entities;
using User.Persistence.Db;
using User.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace User.Persistence.Repositories;

public class RoleRepository : IRoleRepository
{
  private readonly UserDbContext _userDb;

  public RoleRepository(UserDbContext userDb)
    => _userDb = userDb;

  public async Task<Role?> Get(int id, bool deepLoad = false)
  {
    Role? role;

    if (deepLoad)
      role = await _userDb.Roles
          .Include(role => role.Features!)
          .ThenInclude(feature => feature.Permissions)
          .SingleOrDefaultAsync(e => e.RoleId == id);
    else
      role = await _userDb.Roles
          .SingleOrDefaultAsync(e => e.RoleId == id);

    return role;
  }
}