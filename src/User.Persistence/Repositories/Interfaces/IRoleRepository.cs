using User.Domain.Entities;

namespace User.Persistence.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<Role?> Get(int id, bool deepLoad = false);
    }
}
