using Microsoft.EntityFrameworkCore;
using Persistence.Db;
using Persistence.Repositories;
using Persistence.Repositories.Interfaces;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
  public static IServiceCollection AddPersistenceServices(
    this IServiceCollection services,
    string _connectionString)
  {
    #region Repositories
    services.AddTransient<IRoleRepository, RoleRepository>();
    services.AddTransient<IUserRepository, UserRepository>();
    #endregion

    services.AddDbContext<UserDbContext>(o => {
      o.UseSqlite(_connectionString);
    });

    services.AddHealthChecks().AddDbContextCheck<UserDbContext>();

    return services;
  }
}