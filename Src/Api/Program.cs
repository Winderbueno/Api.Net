using Api.Middlewares.Authorization;
using Api.Middlewares.Authentication;
using Api.Middlewares.ErrorHandling;
using Api.Middlewares.Swagger;
using Domain.Configuration;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Security.Claims;
using Claim = Infrastructure.Identity.Constants.Claim;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

#region Configuration
var conf = builder.Configuration;
var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
conf.SetBasePath(Directory.GetCurrentDirectory())
  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
  .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
  .Build();
ApiConf apiConf = conf.GetSection(ApiConf.SectionKey).Get<ApiConf>()!;
services.Configure<ConnectionStrings>(conf.GetSection(ConnectionStrings.SectionKey));
services.Configure<ApiConf>(conf.GetSection(ApiConf.SectionKey));
#endregion

#region Definition
// Swagger/OpenAPI (See. https://aka.ms/aspnetcore/swashbuckle)
services.AddSwaggerGen();
services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGen>();
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

#region Endpoints
services.AddRouting(o => o.LowercaseUrls = true);
services.AddControllers();
services.AddEndpointsApiExplorer();
#endregion

#region Error Handling
// ProblemDetails
services.AddProblemDetails();

// Input Validation (FluentValidation)
services.AddValidatorsFromAssembly(Assembly.Load("Application"));
#endregion

#region Project Services
services.AddApplicationServices();
services.AddInfrastructureServices();
services.AddPersistenceServices(conf.GetConnectionString("DefaultConnection")!);
#endregion

var app = builder.Build();

#region Request Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(o => SwaggerUIMiddleware.Options(o));
}

app.UseExceptionHandler(new ExceptionHandlerOptions()
{
  AllowStatusCode404Response = true,
  ExceptionHandler = async httpCtx => ExceptionMiddleware.ExceptionHandler(httpCtx)
});

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
#endregion

app.Run();