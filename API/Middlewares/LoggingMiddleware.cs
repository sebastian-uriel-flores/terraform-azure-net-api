namespace DemoAPIAzure.Middlewares;

public class LoggingMiddleware
{
    readonly RequestDelegate next;
    private readonly ILogger<LoggingMiddleware> logger;

    public LoggingMiddleware(RequestDelegate nextRequest, ILogger<LoggingMiddleware> logger)
    {
        next = nextRequest;
        this.logger = logger;
    }    

    public async Task Invoke(HttpContext context)
    {
        logger.LogInformation($"Endpoint: {context.GetEndpoint().DisplayName}");
        await next(context);
    }

}
public static class LoggingMiddlewareExtension
{
    public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LoggingMiddleware>();
    }
}