using tecnosor.cleanarchitecture.common.domain.events;
namespace tecnosor.cleanarchitecture.common.infrastructure.events;

/// <summary>
/// Implementation of the event bus service that manages event subscriptions and emissions.
/// Automatically discovers event handlers and emits domain events to the appropriate handlers.
/// </summary>
/// <author>Alfonso Soria</author>
public class EventBusServiceImpl : IEventBusService
{
    private readonly Dictionary<Type, IList<Delegate>> _subscribers = new Dictionary<Type, IList<Delegate>>();
    private readonly object _lock = new object();
    private readonly IEventDiscovery _eventDiscovery = new EventDiscovery(new DefaultAssemblyProvider());

    /// <summary>
    /// Constructor of <see cref="EventBusServiceImpl"/>.
    /// Automatically discovers all types implementing <see cref="IDomainEvent"/> and subscribes their handlers.
    /// </summary>
    public EventBusServiceImpl()
    {
        // TODO: ADD LOGS Properly

        subscribeAllHandlers();
    }

    /// <summary>
    /// Emits a domain event to all subscribed handlers for the event type.
    /// If no handlers are found, it dynamically discovers and subscribes them.
    /// Handlers are executed asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of domain event.</typeparam>
    /// <param name="domainEvent">The domain event instance to emit.</param>
    public void Emit<T>(T domainEvent) where T : IDomainEvent
    {
        Type eventType = typeof(T);
        IList<Delegate> handlers;

        lock (_lock)
        {
            if (!_subscribers.TryGetValue(eventType, out handlers))
            {
                // Instantiate and subscribe handlers on first Emit, if not already done
                handlers = _eventDiscovery.DiscoverAndCreateHandlers<T>();
                _subscribers[eventType] = handlers;
            }
        }

        if (handlers != null)
        {
            foreach (var handler in handlers)
            {
                Task.Run(() =>
                {
                    try
                    {
                        ((Action<T>)handler)(domainEvent);
                    }
                    catch (Exception ex)
                    {
                        // Log or handle the exception as necessary - TODO, use logging module
                        Console.WriteLine($"Error handling event {typeof(T)}: {ex.Message}");
                    }
                });
            }
        }
    }

    /// <summary>
    /// Subscribes handlers for a specific domain event type.
    /// This is a helper method used to add handlers for a specific <see cref="IDomainEvent"/> to the subscriber list.
    /// </summary>
    /// <typeparam name="T">The type of the domain event that the handler subscribes to.</typeparam>
    private void subscribeForType<T>() where T : IDomainEvent
    {
        lock (_lock)
        {
            var handlers = _eventDiscovery.DiscoverAndCreateHandlers<T>();
            if (handlers.Any())
            {
                _subscribers[typeof(T)] = handlers;
            }
        }
    }

    /// <summary>
    /// Subscribes all handlers for the discovered domain events.
    /// </summary>
    private void subscribeAllHandlers()
    {
        var domainEventTypes = _eventDiscovery.DiscoverDomainEventTypes();

        foreach (var eventType in domainEventTypes)
        {
            var subscribeMethod = GetType().GetMethod(nameof(subscribeForType), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var genericSubscribeMethod = subscribeMethod.MakeGenericMethod(eventType);
            genericSubscribeMethod.Invoke(this, null);
        }
    }

}
