using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Api.Middlewares.Authorization;

public static class PolicyBuilder
{
  private static readonly string ClaimCreate = "create";
  private static readonly string ClaimRead = "read";
  private static readonly string ClaimUpdate = "update";
  private static readonly string ClaimDelete = "delete";

  /// <summary>
  /// Creates policies with names "{entity}.(create | read | update | delete)"
  /// </summary>
  public static void AddCrudPolicy(this AuthorizationOptions options, string entity)
  {
    options.AddCreatePolicy(entity);
    options.AddReadPolicy(entity);
    options.AddUpdatePolicy(entity);
    options.AddDeletePolicy(entity);
  }

  public static void AddCreatePolicy(this AuthorizationOptions options, string entity)
      => options.AddPolicy(entity, ClaimCreate);

  public static void AddReadPolicy(this AuthorizationOptions options, string entity)
      => options.AddPolicy(entity, ClaimRead);

  public static void AddUpdatePolicy(this AuthorizationOptions options, string entity)
      => options.AddPolicy(entity, ClaimUpdate);

  public static void AddDeletePolicy(this AuthorizationOptions options, string entity)
      => options.AddPolicy(entity, ClaimDelete);


  private static void AddPolicy(this AuthorizationOptions options, string policyName, string claimName)
      => options.AddPolicy(
          $"{policyName}.{claimName}",
          policy => policy.RequireClaim(ClaimTypes.Role, $"{policyName}.{claimName}"));
}