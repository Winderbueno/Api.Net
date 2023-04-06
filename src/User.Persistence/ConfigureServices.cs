using Microsoft.EntityFrameworkCore;
using User.Persistence.DbContexts;
using User.Persistence.DbContexts.Interfaces;
using User.Persistence.Repositories;
using User.Persistence.Repositories.Interfaces;

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
      o.UseSqlServer(_connectionString, x => x.MigrationsAssembly("Bisa.Persistence"));
    });

    services.AddHealthChecks().AddDbContextCheck<UserDbContext>();

    services.AddScoped<IUserDbContext>(provider => provider.GetService<UserDbContext>()!);

    return services;
  }
}