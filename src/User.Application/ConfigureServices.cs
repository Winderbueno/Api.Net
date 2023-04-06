using User.Application.Services;
using User.Application.Services.Interfaces;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
  public static IServiceCollection AddApplicationServices(this IServiceCollection services)
  {
    #region Mappers
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    #endregion

    #region Services
    services.AddTransient<IUserService, UserService>();
    #endregion

    return services;
  }
}