using Microsoft.AspNetCore.Mvc;

namespace GlobalErrorHandlingSample.Middleware;

public class GlobalErrorHandlingMiddleware
{
    private ILogger<GlobalErrorHandlingMiddleware> _logger;
    private RequestDelegate _next;

    public GlobalErrorHandlingMiddleware(ILogger<GlobalErrorHandlingMiddleware> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "An error occured : {Message}", exception.Message);
            ProblemDetails problemDetails = new()
            {
                Title = "Server Error",
                Status = StatusCodes.Status500InternalServerError,
            };

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}