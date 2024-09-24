using tecnosor.cleanarchitecture.common.domain.events;
using tecnosor.cleanarchitecture.common.infrastructure.events;

namespace tecnosor.cleanarchitecture.common.tests.unit.infrastructure.events;

// Mock event for testing purposes
public class TestDomainEvent : IDomainEvent
{
    public EEVentType EventType => EEVentType.LOCAL;
    public string Topic => "example.event";
}

// Mock event handler for testing purposes
public class TestEventHandler : IEventHandler<TestDomainEvent>
{
    public void Handle(TestDomainEvent domainEvent)
    {
        // Implementation doesn't matter for this test
    }
}

[TestClass]
public class EventDiscoveryUnitTests
{
    private Mock<IAssemblyProvider> _mockAssemblyProvider;
    private Mock<Assembly> _mockAssembly;

    [TestInitialize]
    public void Setup()
    {
        _mockAssemblyProvider = new Mock<IAssemblyProvider>();
        _mockAssembly = new Mock<Assembly>();
    }

    [TestMethod]
    public void DiscoverAndCreateHandlers_ShouldReturnEventHandlers_WhenHandlersExist()
    {
        // Arrange
        SetupMockAssemblyProviderWithTypes(typeof(TestEventHandler));

        var eventDiscovery = new EventDiscovery(_mockAssemblyProvider.Object);

        // Act
        var handlers = eventDiscovery.DiscoverAndCreateHandlers<TestDomainEvent>();

        // Assert
        Assert.AreEqual(1, handlers.Count);  // Ensure one handler was found
        Assert.IsInstanceOfType(handlers.First(), typeof(Action<TestDomainEvent>));  // Ensure it's an Action delegate
    }

    [TestMethod]
    public void DiscoverAndCreateHandlers_ShouldReturnEmptyList_WhenNoHandlersExist()
    {
        // Arrange
        SetupMockAssemblyProviderWithTypes();

        var eventDiscovery = new EventDiscovery(_mockAssemblyProvider.Object);

        // Act
        var handlers = eventDiscovery.DiscoverAndCreateHandlers<TestDomainEvent>();

        // Assert
        Assert.AreEqual(0, handlers.Count);  // No handlers found
    }

    [TestMethod]
    public void DiscoverDomainEventTypes_ShouldReturnDomainEventTypes_WhenTheyExist()
    {
        // Arrange
        SetupMockAssemblyProviderWithTypes(typeof(TestDomainEvent));

        var eventDiscovery = new EventDiscovery(_mockAssemblyProvider.Object);

        // Act
        var domainEventTypes = eventDiscovery.DiscoverDomainEventTypes();

        // Assert
        Assert.AreEqual(1, domainEventTypes.Count);  // Ensure one domain event type was found
        Assert.AreEqual(typeof(TestDomainEvent), domainEventTypes.First());  // Ensure it's the correct domain event type
    }

    [TestMethod]
    public void DiscoverDomainEventTypes_ShouldReturnEmptyList_WhenNoDomainEventsExist()
    {
        // Arrange
        SetupMockAssemblyProviderWithTypes();

        var eventDiscovery = new EventDiscovery(_mockAssemblyProvider.Object);

        // Act
        var domainEventTypes = eventDiscovery.DiscoverDomainEventTypes();

        // Assert
        Assert.AreEqual(0, domainEventTypes.Count);  // No domain event types found
    }

    /// <summary>
    /// Sets up the mock assembly provider to return a mock assembly with specific types.
    /// </summary>
    /// <param name="types">The types that the mock assembly should return.</param>
    private void SetupMockAssemblyProviderWithTypes(params Type[] types)
    {
        // Mock the behavior of GetTypes() to return the provided types
        _mockAssembly.Setup(a => a.GetTypes()).Returns(types);

        // Set up the assembly provider to return this mock assembly
        _mockAssemblyProvider.Setup(p => p.GetAssemblies()).Returns(new[] { _mockAssembly.Object });
    }
}
