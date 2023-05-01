using User.Domain.Entities;

namespace User.Persistence.Repositories.Interfaces;

public interface IRoleRepository
{
  Task<Role?> GetAsync(int id, bool deepLoad = false);
}