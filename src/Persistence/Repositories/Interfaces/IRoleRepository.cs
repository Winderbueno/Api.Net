using Domain.Entities;

namespace Persistence.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<Role?> Get(int id, bool deepLoad = false);
    }
}
