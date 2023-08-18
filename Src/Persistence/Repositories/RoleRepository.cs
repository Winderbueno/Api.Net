using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Db;
using Persistence.Repositories.Interfaces;

namespace Persistence.Repositories;

public class RoleRepository : IRoleRepository
{
  private readonly UserDbContext _userDb;

  public RoleRepository(UserDbContext userDb)
    => _userDb = userDb;

  public async Task<Role?> GetAsync(int id, bool deepLoad = false)
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