using System.Net;
using System.Text.Json;
using PulseFit.BLL.Models;

namespace PulseFit.Middlewares;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        int statusCode;
        ErrorBody error;

        switch (ex)
        {
            case KeyNotFoundException:
                statusCode = (int)HttpStatusCode.NotFound;
                error = new ErrorBody("Not Found", ex.Message, "NOT_FOUND");
                break;

            case UnauthorizedAccessException:
                statusCode = (int)HttpStatusCode.Unauthorized;
                error = new ErrorBody("Unauthorized", ex.Message, "UNAUTHORIZED");
                break;

            case ArgumentException:
                statusCode = (int)HttpStatusCode.BadRequest;
                error = new ErrorBody("Bad Request", ex.Message, "BAD_REQUEST");
                break;

            default:
                statusCode = (int)HttpStatusCode.InternalServerError;
                error = new ErrorBody("Internal Server Error", "An unexpected error occurred.", "INTERNAL_ERROR");
                break;
        }

        var response = new ErrorResponse(statusCode, error);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await context.Response.WriteAsync(json);
    }
}
