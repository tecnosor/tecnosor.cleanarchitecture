using tecnosor.cleanarchitecture.common.domain.events;

namespace tecnosor.cleanarchitecture.common.infrastructure.events;

/// <summary>
/// Interface for discovering event handlers and domain event types.
/// </summary>
/// <author>Alfonso Soria</author>
public interface IEventDiscovery
{
    /// <summary>
    /// Discovers and creates handlers for a specified domain event type.
    /// Searches through all loaded assemblies to find classes that implement <see cref="IEventHandler{T}"/>
    /// for the specified domain event type.
    /// </summary>
    /// <typeparam name="T">The type of domain event.</typeparam>
    /// <returns>A list of delegates representing the event handlers for the domain event type.</returns>
    IList<Delegate> DiscoverAndCreateHandlers<T>() where T : IDomainEvent;

    /// <summary>
    /// Discovers all domain event types that implement <see cref="IDomainEvent"/>.
    /// Searches through all loaded assemblies to find classes that inherit from <see cref="IDomainEvent"/>.
    /// </summary>
    /// <returns>A list of types that represent the domain events in the current application domain.</returns>
    IList<Type> DiscoverDomainEventTypes();
}

public interface IAssemblyProvider
{
    IEnumerable<Assembly> GetAssemblies();
}

internal class DefaultAssemblyProvider : IAssemblyProvider
{
    public IEnumerable<Assembly> GetAssemblies()
    {
        return AppDomain.CurrentDomain.GetAssemblies();
    }
}

/// <summary>
/// Concrete implementation of <see cref="IEventDiscovery"/> that uses reflection to discover event handlers
/// and domain event types from the currently loaded assemblies.
/// </summary>
public class EventDiscovery : IEventDiscovery
{
    private readonly IAssemblyProvider _assemblyProvider;
    public EventDiscovery(IAssemblyProvider assemblyProvider)
    {
        _assemblyProvider = assemblyProvider;
    }

    /// <summary>
    /// Discovers and creates handlers for a given domain event type.
    /// Searches through all loaded assemblies to find classes that implement <see cref="IEventHandler{T}"/>
    /// for the specified domain event type.
    /// </summary>
    /// <typeparam name="T">The type of domain event.</typeparam>
    /// <returns>A list of delegates representing the event handlers for the domain event type.</returns>
    public IList<Delegate> DiscoverAndCreateHandlers<T>() where T : IDomainEvent
    {
        var handlerType = typeof(IEventHandler<T>);
        var handlerInstances = _assemblyProvider
            .GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => handlerType.IsAssignableFrom(t) && !t.IsAbstract)
            .Select(t => Activator.CreateInstance(t))
            .Cast<IEventHandler<T>>()
            .Select(h => new Action<T>(h.Handle))
            .ToList();

        return handlerInstances.Cast<Delegate>().ToList();
    }

    /// <summary>
    /// Discovers all domain event types that implement <see cref="IDomainEvent"/>.
    /// Searches through all loaded assemblies to find classes that inherit from <see cref="IDomainEvent"/>.
    /// </summary>
    /// <returns>A list of types that represent the domain events in the current application domain.</returns>
    public IList<Type> DiscoverDomainEventTypes() => _assemblyProvider
                                                              .GetAssemblies()
                                                              .SelectMany(a => a.GetTypes())
                                                              .Where(t => typeof(IDomainEvent).IsAssignableFrom(t) && !t.IsAbstract)
                                                              .ToList();
    
}