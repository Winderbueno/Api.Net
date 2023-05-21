using Swashbuckle.AspNetCore.SwaggerUI;

namespace User.Api.Middlewares.Swagger;

public static class SwaggerUIMiddleware
{
  public static void Options(SwaggerUIOptions o)
  {
    // SwaggerUI as OAuth Client
    // https://github.com/domaindrivendev/Swashbuckle.AspNetCore/blob/master/README.md#enable-oauth20-flows
    o.OAuthClientId("Client.ApiSwaggerId"); // Todo
    o.OAuthScopes(new string[] { "Scope.Api" }); // Todo Scope as Cste
    o.OAuthAppName("Api Swagger");
    o.OAuthUsePkce();

    // Api Endpoint
    o.SwaggerEndpoint($"/swagger/v1/swagger.json", "V1");
  }
}