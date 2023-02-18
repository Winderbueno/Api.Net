using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.DbContexts.Interfaces
{
    public interface IApiDbContext : IDisposable
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}