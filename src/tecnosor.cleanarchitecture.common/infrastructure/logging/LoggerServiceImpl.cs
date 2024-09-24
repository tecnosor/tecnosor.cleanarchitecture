using Microsoft.Extensions.Options;
using Serilog;
using tecnosor.cleanarchitecture.common.domain.logging;
namespace tecnosor.cleanarchitecture.common.infrastructure.logging;

public class LoggerServiceImpl<T> : ILoggerService<T>
{ 
    private readonly ILogger _logger = Log.ForContext<T>();
    private readonly IOptionsMonitor<LoggingOptions> _loggingOptions;

    public LoggerServiceImpl(IOptionsMonitor<LoggingOptions> loggingOptions) => _loggingOptions = loggingOptions;

    public void LogInformation(string message)
    {
        if (_loggingOptions.CurrentValue.EnableInfoTraces) return;
        _logger.Information(message);
    }

    public void LogWarning(string message) => _logger.Warning(message);
    public void LogError(string message, Exception ex) => _logger.Error(ex, message);
    public void LogDebug(string message)
    {
        if (!_loggingOptions.CurrentValue.EnableDebugTraces) return;
        _logger.Debug(message);
    }
    public void LogError(string message) => _logger.Error(message);
}
