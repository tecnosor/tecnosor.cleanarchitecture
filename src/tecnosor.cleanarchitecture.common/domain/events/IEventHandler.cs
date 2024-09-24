namespace tecnosor.cleanarchitecture.common.domain.events;

public interface IEventHandler<T> where T : IDomainEvent
{
    void Handle(T domainEvent);
}
