using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Security.Claims;
using User.Api.Configuration;
using User.Api.Middlewares.Authorization;
using User.Api.Middlewares.Authentication;
using User.Api.Middlewares.Swagger;
using Claim = User.Infrastructure.Identity.Constants.Claim;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

#region Api Definition
// Swagger/OpenAPI (See. https://aka.ms/aspnetcore/swashbuckle)
services.AddSwaggerGen();
services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGen>();
#endregion

#region Api Configuration
var confBuilder = builder.Configuration;
confBuilder.SetBasePath(Directory.GetCurrentDirectory()+"\\src\\User.Api")
  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
  .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
  .Build();

ApiConf apiConf = confBuilder.GetSection(ApiConf.SectionKey).Get<ApiConf>()!;
services.Configure<ConnectionStrings>(confBuilder.GetSection(ConnectionStrings.SectionKey));
services.Configure<ApiConf>(confBuilder.GetSection(ApiConf.SectionKey));
#endregion

#region Authentication
// AccessToken validation
// https://learn.microsoft.com/en-us/aspnet/core/security/authentication/?view=aspnetcore-6.0#authentication-scheme
services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", o =>
    { // Jwt Token Scheme
      o.Authority = apiConf.AuthorityUrl; // Authentication Authority Server (In our case Identity Server)
      o.TokenValidationParameters.ValidateAudience = false;
    });

services.AddScoped<IClaimsTransformation, ClaimsTransformation>();
#endregion

#region Authorization
services.AddAuthorization(o => {
  o.AddPolicy("user", policy => policy.RequireClaim(ClaimTypes.Role, Claim.User));
  o.AddCrudPolicy("user");
});
#endregion

// Add services to the container.
services.AddControllers();
services.AddEndpointsApiExplorer();

var app = builder.Build();

#region Http Request Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(o => SwaggerMiddleware.SwaggerUIConf(o));
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
#endregion

app.Run();