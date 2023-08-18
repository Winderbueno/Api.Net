using Application.Mappers.Interfaces;
using Application.Services;
using Application.Services.Interfaces;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
  public static IServiceCollection AddApplicationServices(this IServiceCollection services)
  {
    #region Mappers
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    services.AddTransient<IUserMapper, UserMapper>();
    #endregion

    #region Services
    services.AddTransient<IUserService, UserService>();
    #endregion

    return services;
  }
}