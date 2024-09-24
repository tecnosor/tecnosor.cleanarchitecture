using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Text;
using tecnosor.cleanarchitecture.common.domain.logging;

namespace tecnosor.cleanarchitecture.common.infrastructure.logging;

internal sealed class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILoggerService<RequestLoggingMiddleware> _logger;
    private readonly IOptionsMonitor<LoggingOptions> _loggingOptions;

    public RequestLoggingMiddleware(RequestDelegate next, ILoggerService<RequestLoggingMiddleware> logger, IOptionsMonitor<LoggingOptions> loggingOptions)
    {
        _next = next;
        _logger = logger;
        _loggingOptions = loggingOptions;
    }

    public async Task Invoke(HttpContext context)
    {
        await _next(context);
        _logger.LogInformation($"[audit]\t Outgoing response: {context.Response.StatusCode}");
    }

    private async Task TraceAuditIn(HttpContext context) 
    {
        if (!_loggingOptions.CurrentValue.EnableAuditRequests) return;
        var dict = new Dictionary<string, string>();
        dict["LogType"] = "Tecnosor.Common.Audit";
        dict["Call"] = "In";
        dict["LogContent"] = await context.ToStringShortAsync(); ;
        _logger.LogInformation($"{JsonSerializer.Serialize(dict)}");
    }

    private async Task TraceAuditOut(HttpContext context)
    {
        if (!_loggingOptions.CurrentValue.EnableAuditRequests) return;
        var dict = new Dictionary<string, string>();
        dict["LogType"] = "Tecnosor.Common.Audit";
        dict["Call"] = "Out";
        dict["LogContent"] = await context.SerializeResponseShortAsync();
        _logger.LogInformation($"{JsonSerializer.Serialize(dict)}");
    }

    private async Task TraceQrIn(HttpContext context)
    {
        if (!_loggingOptions.CurrentValue.EnableQr) return;
        var dict = new Dictionary<string, string>();
        dict["LogType"] = "Tecnosor.Common.Qr";
        dict["Call"] = "In";
        dict["LogContent"] = await context.ToStringCompleteAsync();
        _logger.LogInformation($"{JsonSerializer.Serialize(dict)}");        
    }

    private async Task TraceQrOut(HttpContext context)
    {
        if (!_loggingOptions.CurrentValue.EnableQr) return;
        var dict = new Dictionary<string, string>();
        dict["LogType"] = "Tecnosor.Common.Qr";
        dict["Call"] = "Out";
        dict["LogContent"] = await context.SerializeResponseCompleteAsync();
        _logger.LogInformation($"{JsonSerializer.Serialize(dict)}");
    }
}

internal record HttpContextIn(
    string RequestPath,
    string RequestMethod,
    string UserAgent,
    string QueryString,
    string RemoteIpAddress,
    Dictionary<string, string> Headers,
    string Body
);

internal record HttpContextOut(
    string Body,
    string StatusCode
);

public static class HttpContextExtensions
{
    public static async Task<string> ToStringShortAsync(this HttpContext context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));

        var headers = new Dictionary<string, string>();
        foreach (var header in context.Request.Headers)
        {
            headers[header.Key] = header.Value.ToString();
        }

        return JsonSerializer.Serialize(new HttpContextIn(
                                                RequestPath: context.Request.Path.ToString(),
                                                RequestMethod: context.Request.Method,
                                                UserAgent: context.Request.Headers["User-Agent"].ToString(),
                                                QueryString: context.Request.QueryString.ToString(),
                                                RemoteIpAddress: context.Connection.RemoteIpAddress?.ToString(),
                                                Headers: headers,
                                                Body: "N/A"
                                            ));
    }
    public static async Task<string> ToStringCompleteAsync(this HttpContext context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));

        var headers = new Dictionary<string, string>();
        foreach (var header in context.Request.Headers)
        {
            headers[header.Key] = header.Value.ToString();
        }

        string body = null;
        context.Request.EnableBuffering();

        var contentType = context.Request.ContentType?.ToLower();
        if (contentType != null &&
            (contentType.Contains("text/") ||
             contentType.Contains("application/json") ||
             contentType.Contains("application/xml")))
        {
            using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true))
            {
                body = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0;
            }
        }

        return JsonSerializer.Serialize(new HttpContextIn(
                                                RequestPath: context.Request.Path.ToString(),
                                                RequestMethod: context.Request.Method,
                                                UserAgent: context.Request.Headers["User-Agent"].ToString(),
                                                QueryString: context.Request.QueryString.ToString(),
                                                RemoteIpAddress: context.Connection.RemoteIpAddress?.ToString(),
                                                Headers: headers,
                                                Body: body
                                            ));
    }

    public static async Task<string> SerializeResponseCompleteAsync(this HttpContext context)
    {
        // contemplate if include, headers...
        var statusCode = context.Response.StatusCode.ToString();
        string body;
        context.Response.Body.Position = 0;
        using (var reader = new StreamReader(context.Response.Body, Encoding.UTF8, leaveOpen: true))
        {
            body = await reader.ReadToEndAsync();
            context.Response.Body.Position = 0;
        }

        return JsonSerializer.Serialize(new HttpContextOut(
            Body: body,
            StatusCode: statusCode
        ));
    }

    // contemplate if include headers...
    public static async Task<string> SerializeResponseShortAsync(this HttpContext context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));
        var statusCode = context.Response.StatusCode.ToString();

        return JsonSerializer.Serialize(new HttpContextOut(
            Body: "N/A",
            StatusCode: statusCode
        ));
    }
}