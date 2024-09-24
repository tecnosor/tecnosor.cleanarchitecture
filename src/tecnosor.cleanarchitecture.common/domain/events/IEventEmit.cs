namespace tecnosor.cleanarchitecture.common.domain.events;

public interface IEventEmit
{
    public void Emit<T>(T d) where T : IDomainEvent;
}
