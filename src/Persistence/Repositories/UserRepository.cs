using Domain.Entities;
using Persistence.DbContexts;
using Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public  class UserRepository: IUserRepository
    {
        private readonly ApiDbContext _apiDb;

        public UserRepository(ApiDbContext apiDb) => _apiDb = apiDb;

        public List<int> GetUsers() => _apiDb.Users.Select(u => u.UserId).ToList();

        public User? GetUserById(int id, bool deepLoad = false)
        {
            User user;

            if (deepLoad)
                user = _apiDb.Users
                    .Include(user => user.Role)
                    .ThenInclude(role => role.Features)
                    .ThenInclude(feature => feature.Permissions)
                    .SingleOrDefault(e => e.UserId == id);
            else 
                user = _apiDb.Users
                    .SingleOrDefault(e => e.UserId == id);

            return user;
        }

        public User AddUser(User user)
        {
            _apiDb.Add(user);
            _apiDb.SaveChanges();
            return user;
        }
    }
}
