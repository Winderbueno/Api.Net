﻿using Api.Configuration;
using Api.Middlewares.Swagger.Filters;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Api.Middlewares.Swagger
{
    public class ConfigureSwaggerGen : IConfigureOptions<SwaggerGenOptions>
    {
        public AppConf AppConf { get; }

        public ConfigureSwaggerGen(IConfiguration configuration)
          => AppConf = configuration.GetSection(AppConf.SectionKey).Get<AppConf>()!;
        

        public void Configure(SwaggerGenOptions o)
        {
            // Add Api Doc in SwaggerUI based on Controller & Models comment
            // var xmlFile = $"{ Assembly.GetExecutingAssembly().GetName().Name }.xml";
            // var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            // o.IncludeXmlComments(xmlPath);

            // Add 'Username' input on all operation
            if(AppConf.Environment != Env.Dev)
                o.OperationFilter<RequiredParameterFilter>();

            // Convert enum in schema definition
            o.SchemaFilter<EnumTypesSchemaFilter>();

            // Api Doc
            o.SwaggerDoc(
              "v1",
              new OpenApiInfo() {
                Title = $"Api Description",
                Version = "V1",
                Contact = new OpenApiContact { Email = "kevin.gellenoncourt@gmail.com" }
              });

            ConfigureSecurity(o);
        }

        // https://github.com/domaindrivendev/Swashbuckle.AspNetCore/blob/master/README.md#add-security-definitions-and-requirements
        private void ConfigureSecurity(SwaggerGenOptions o)
        {
            o.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme() {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows()
                {
                    AuthorizationCode = new OpenApiOAuthFlow()
                    {
                        AuthorizationUrl = new Uri($"{AppConf.AuthorityUrl}/connect/authorize"),
                        TokenUrl = new Uri($"{AppConf.AuthorityUrl}/connect/token"), 
                        Scopes = new Dictionary<string, string> { { "Scope.Api", "Api" } } // Todo - Scope in Cste
                    }
                }
            });

            o.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                            Id = "oauth2"
                        }
                    },
                    new[] { "Scope.Api" }
                },
            });
        }
    }
}
