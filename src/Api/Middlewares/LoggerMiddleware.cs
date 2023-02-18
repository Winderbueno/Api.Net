using Microsoft.IO;

namespace Bisa.Api.Middlewares
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<LoggerMiddleware> logger;
        private readonly RecyclableMemoryStreamManager recyclableMemoryStreamManager;

        public LoggerMiddleware(
            RequestDelegate next,
            ILogger<LoggerMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
            recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        public async Task InvokeAsync(HttpContext ctx)
        {
            var resp = ctx.Response;
            var originalBodyStream = resp.Body;

            await using var responseBody = recyclableMemoryStreamManager.GetStream();
            resp.Body = responseBody;

            await next(ctx);

            resp.Body.Seek(0, SeekOrigin.Begin);

            var req = ctx.Request;
            if (resp.StatusCode == StatusCodes.Status400BadRequest
                || resp.StatusCode == StatusCodes.Status404NotFound
                || resp.StatusCode == StatusCodes.Status500InternalServerError)
            {
                var respBody = await new StreamReader(resp.Body).ReadToEndAsync();
                resp.Body.Seek(0, SeekOrigin.Begin);

                logger.LogError(
                    @$"Http Response Information:{Environment.NewLine}
                            Schema: {req.Scheme}
                            Host: {req.Host} 
                            Path: {req.Path} 
                            QueryString: {req.QueryString}
                            Response Body: {respBody}");
            }

            await responseBody.CopyToAsync(originalBodyStream);
        }
    }
}