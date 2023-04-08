using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace User.Api.Middlewares.Swagger.Filters;

public class EnumTypesSchemaFilter : ISchemaFilter
{
  public void Apply(OpenApiSchema schema, SchemaFilterContext context)
  {
    if (context.Type != null && context.Type.IsEnum)
    {
      var enumNames = new OpenApiArray();

      foreach (var enumValue in Enum.GetValues(context.Type))
      {
        if (enumValue != null)
        {
          schema.Description += $"{Convert.ToInt32(enumValue)} = {enumValue}";
          schema.Description += Environment.NewLine;

          enumNames.Add(new OpenApiString(enumValue.ToString()));
        }
      }

      schema.Extensions.Add("x-enumNames", enumNames);
    }
  }
}