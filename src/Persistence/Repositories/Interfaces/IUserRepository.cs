using Domain.Entities;

namespace Persistence.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<int>> Get();
        Task<User?> Get(int id, bool deepLoad = false);
        Task<User?> GetByIdentityId(Guid identityId);
    }
}
