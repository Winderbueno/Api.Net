using User.Api.Configuration;
using Microsoft.Extensions.Options;

namespace User.Api.Middlewares.ErrorHandling
{
    public class ConfigureProblemDetailsOptions : IConfigureOptions<ProblemDetailsOptions>
    {
        public ApiConf ApiConf { get; }

        public ConfigureProblemDetailsOptions(IConfiguration configuration)
          => ApiConf = (configuration.GetSection(ApiConf.SectionKey).Get<ApiConf>())!;

        public void Configure(ProblemDetailsOptions options)
        {
            //options.ValidationProblemStatusCode = StatusCodes.Status400BadRequest;

            // Custom mapping for FluentValidation Exception
            // options.Map<ValidationException>((ctx, ex) => {

            //     var errors = ex.Errors
            //         .GroupBy(x => x.PropertyName)
            //         .ToDictionary(
            //             x => x.Key,
            //             x => x.Select(x => x.ErrorMessage).ToArray());

            //     var factory = ctx.RequestServices.GetRequiredService<ProblemDetailsFactory>();

            //     return factory.CreateValidationProblemDetails(ctx, errors);
            // });

            // Choose when to include exception details in ProblemDetails
            var env = ApiConf.Environment;
            // options.IncludeExceptionDetails = (ctx, ex) => {
            //     return env == Env.Dev || env == Env.Int;
            // };
        }
    }
}
