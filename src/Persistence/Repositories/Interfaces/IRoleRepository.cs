using Domain.Entities;

namespace Persistence.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Role? GetRoleById(int id, bool deepLoad = false);
    }
}
