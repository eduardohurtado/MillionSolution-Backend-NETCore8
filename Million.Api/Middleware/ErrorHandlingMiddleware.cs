using System.Net;
using System.Text.Json;
using MongoDB.Bson;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger, IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred");

            // Default to Internal Server Error
            var statusCode = HttpStatusCode.InternalServerError;

            // Handle known error types
            if (ex is FormatException && ex.Message.Contains("is not a valid 24 digit hex string"))
            {
                statusCode = HttpStatusCode.BadRequest;
            }
            else if (ex is NotFoundException) // optional: custom exception
            {
                statusCode = HttpStatusCode.NotFound;
            }

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            var response = new
            {
                error = ex.Message,
                stackTrace = _env.IsDevelopment() ? ex.StackTrace : null
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
