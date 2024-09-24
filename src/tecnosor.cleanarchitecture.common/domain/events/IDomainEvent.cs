namespace tecnosor.cleanarchitecture.common.domain.events;

public interface IDomainEvent
{
    [JsonIgnore]
    public EEVentType EventType { get; }
    [JsonIgnore]
    public string Topic { get; }
}

public enum EEVentType
{
    LOCAL,
    EXTERNAL,
    BOTH
}