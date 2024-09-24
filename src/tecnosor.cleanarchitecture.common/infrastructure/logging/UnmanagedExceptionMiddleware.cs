using Microsoft.AspNetCore.Http;
using System.Net;
using tecnosor.cleanarchitecture.common.domain.errors;
using tecnosor.cleanarchitecture.common.domain.logging;

namespace tecnosor.cleanarchitecture.common.infrastructure.logging;
internal sealed class UnmanagedExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILoggerService<UnmanagedExceptionMiddleware> _logger;

    public UnmanagedExceptionMiddleware(RequestDelegate next, ILoggerService<UnmanagedExceptionMiddleware> logger)
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
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        // Registrar la excepción y sus inner exceptions
        var exceptionDetails = GetExceptionDetails(ex);
        _logger.LogError($"An unhandled exception has occurred: {exceptionDetails}", ex);

        var errorDto = new ErrorResponse(
            Message: "An unexpected error occurred. Please try again later.",
            StatusCode: (int)HttpStatusCode.InternalServerError
        );

        var result = JsonSerializer.Serialize(errorDto);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        return context.Response.WriteAsync(result);
    }

    private string GetExceptionDetails(Exception ex)
    {
        var details = new List<string>();
        var currentException = ex;

        while (currentException != null)
        {
            details.Add(currentException.Message);
            details.Add(currentException.StackTrace);
            currentException = currentException.InnerException;
        }

        return string.Join(" -> ", details);
    }
}