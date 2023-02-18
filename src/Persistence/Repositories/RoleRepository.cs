using Domain.Entities;
using Persistence.DbContexts;
using Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Persistence.Repositories
{
    public  class RoleRepository: IRoleRepository
    {
        private readonly ApiDbContext _apiDb;

        public RoleRepository(ApiDbContext apiDb) => _apiDb = apiDb;

        public Role? GetRoleById(int id, bool deepLoad = false)
        {
            Role role;

            if (deepLoad)
                role = _apiDb.Roles
                    .Include(role => role.Features)
                    .ThenInclude(feature => feature.Permissions)
                    .SingleOrDefault(e => e.RoleId == id);
            else
                role = _apiDb.Roles
                    .SingleOrDefault(e => e.RoleId == id);

            return role;
        }
    }
}
