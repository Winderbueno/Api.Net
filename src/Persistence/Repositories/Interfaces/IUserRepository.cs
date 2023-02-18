using Domain.Entities;

namespace Persistence.Repositories.Interfaces
{
    public interface IUserRepository
    {
        List<int> GetUsers();
        User? GetUserById(int id, bool deepLoad = false);
        User AddUser(User user);
    }
}
