using User.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace User.Persistence.DbContexts.Interfaces
{
    public interface IUserDbContext : IDisposable
    {
        public DbSet<UserK> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}