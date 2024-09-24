using Microsoft.Extensions.Configuration;
using Serilog;

namespace tecnosor.cleanarchitecture.common.infrastructure.logging;

internal class LoggerConfiguration
{
    public static void Configure(IConfiguration configuration)
    {
        var loggingConfig = configuration.GetSection("Tecnosor:CleanArchitecture:Logging");

        // Verificar si el logging está habilitado
        if (!loggingConfig.GetValue<bool>("Enabled"))
        {
            return; // Salir si el logging no está habilitado
        }

        // Configuración básica de Serilog
        var loggerConfiguration = new Serilog.LoggerConfiguration()
                                             .MinimumLevel.Debug()
                                             .Enrich.FromLogContext();

        // Configurar diferentes tipos de logging basado en el tipo configurado
        var logType = loggingConfig.GetValue<string>("Type");
        switch (logType.ToLower())
        {
            case "console":
                loggerConfiguration.WriteTo.Console();
                break;

            case "file":
                var fileConf = loggingConfig.GetSection("FileConf");
                var path = fileConf.GetValue<string>("Path");
                var retentionEnabled = fileConf.GetValue<bool>("Retention:Enabled");
                var retentionUnit = fileConf.GetValue<string>("Retention:Unit");
                var retentionValue = fileConf.GetValue<int>("Retention:Value");

                // Configurar archivo de logs
                loggerConfiguration.WriteTo.File(path, rollingInterval: RollingInterval.Day);
                if (retentionEnabled)
                {
                    // Implementar lógica de retención aquí
                    // Por ejemplo, podrías usar el sink de Serilog para manejar la retención
                }
                break;
            default:
                throw new NotSupportedException($"Log type '{logType}' is not supported.");
        }

        // Finalizar configuración de logger
        Log.Logger = loggerConfiguration.CreateLogger();
    }
}
