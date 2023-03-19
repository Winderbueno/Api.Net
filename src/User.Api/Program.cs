using User.Api.Configuration;
using User.Api.Middlewares.Swagger;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

#region Api | Configuration
var confBuilder = builder.Configuration;
confBuilder.SetBasePath(Directory.GetCurrentDirectory()+"\\src\\User.Api")
  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
  .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
  .Build();

var apiConf = confBuilder.GetSection(ApiConf.SectionKey).Get<ApiConf>();
builder.Services.Configure<ConnectionStrings>(confBuilder.GetSection(ConnectionStrings.SectionKey));
builder.Services.Configure<ApiConf>(confBuilder.GetSection(ApiConf.SectionKey));
#endregion

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGen>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(o => SwaggerMiddleware.SwaggerUIConf(o));
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
