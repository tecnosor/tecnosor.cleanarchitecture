namespace tecnosor.cleanarchitecture.common.domain.logging;

public class LoggingOptions
{
    public bool Enabled { get; set; } = false;
    public string Type { get; set; } = "console"; // Default value
    public FileSettings FileConf { get; set; } = new FileSettings();
    public bool EnableQr { get; set; } = true; // Default value based on environment
    public bool EnableAuditRequests { get; set; } = true; // Default value
    public bool EnableDebugTraces { get; set; } = true; // Default value based on environment
    public bool EnableInfoTraces { get; set; } = true; // Default value
    public class FileSettings
    {
        public string Path { get; set; } = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/logs/{AppDomain.CurrentDomain.FriendlyName}";
        public RetentionSettings Retention { get; set; } = new RetentionSettings();
        public class RetentionSettings
        {
            public bool Enabled { get; set; } = false;
            public string Unit { get; set; } = "monthly"; // Default value
            public int Value { get; set; } = 1; // Default value
        }
    }
}

