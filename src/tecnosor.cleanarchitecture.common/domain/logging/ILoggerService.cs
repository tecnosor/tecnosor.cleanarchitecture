namespace tecnosor.cleanarchitecture.common.domain.logging;

public interface ILoggerService<T>
{
    void LogInformation(string message);
    void LogWarning(string message);
    void LogError(string message, Exception ex);
    void LogError(string message);
    void LogDebug(string message);
}
