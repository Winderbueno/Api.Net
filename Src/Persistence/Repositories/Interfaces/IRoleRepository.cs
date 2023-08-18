using Domain.Entities;

namespace Persistence.Repositories.Interfaces;

public interface IRoleRepository
{
  Task<Role?> GetAsync(int id, bool deepLoad = false);
}