using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace User.Api.Middlewares.Swagger.Filters
{
    /// <summary>
    /// Adds the field username on swagger which is required by the API
    /// </summary>
    public class RequiredParameterFilter : IOperationFilter
    {
        /// <summary>
        /// Implement IOperationFilter.Apply method
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "username",
                In = ParameterLocation.Header,
                Description = "username",
                Required = true,
                Schema = new OpenApiSchema
                {
                    Type = "string",
                    Default = new OpenApiString("")
                }
            });
        }
    }
}
