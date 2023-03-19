using User.Domain.Entities;

namespace User.Persistence.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<int>> Get();
        Task<UserK?> Get(int id, bool deepLoad = false);
        Task<UserK?> GetByIdentityId(Guid identityId);
    }
}
