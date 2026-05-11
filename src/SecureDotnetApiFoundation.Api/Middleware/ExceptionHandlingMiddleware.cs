using System.Net;
using System.Text.Json;
namespace SecureDotnetApiFoundation.Api.Middleware;
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger) { _next = next; _logger = logger; }
    public async Task InvokeAsync(HttpContext context)
    {
        try { await _next(context); }
        catch (Exception exception)
        {
            var correlationId = context.Response.Headers["X-Correlation-Id"].ToString();
            _logger.LogError(exception, "Unhandled exception. CorrelationId: {CorrelationId}", correlationId);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(JsonSerializer.Serialize(new { error = "An unexpected error occurred.", correlationId }));
        }
    }
}
