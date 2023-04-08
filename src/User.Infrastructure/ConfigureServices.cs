using User.Infrastructure.Identity.Helpers;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
  public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
  {
    services.AddHttpContextAccessor();
    services.AddTransient<XsrfHandler>();

    return services;
  }
}