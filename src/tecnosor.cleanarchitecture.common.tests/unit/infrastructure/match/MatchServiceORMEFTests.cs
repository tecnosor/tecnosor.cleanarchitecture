using tecnosor.cleanarchitecture.common.domain.match;
using tecnosor.cleanarchitecture.common.infrastructure.match;

namespace tecnosor.cleanarchitecture.common.tests.unit.infrastructure.match;

[TestClass]
public class MatchServiceORMEFTests
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime BirthDate { get; set; }
    }


    private IQueryable<Person> _data;
    private MatchServiceORMEF<Person> _service;

    [TestInitialize]
    public void Setup()
    {
        _data = new List<Person>
        {
            new Person { Name = "John Doe", Age = 30, BirthDate = new DateTime(1990, 1, 1) },
            new Person { Name = "Jane Smith", Age = 25, BirthDate = new DateTime(1995, 2, 2) },
            new Person { Name = "Alice Johnson", Age = 35, BirthDate = new DateTime(1985, 3, 3) },
        }.AsQueryable();

        _service = new MatchServiceORMEF<Person>(_data);
    }

    [TestMethod]
    public void MatchQueryable_ShouldReturnCorrectResults_WithSingleFilter()
    {
        // Arrange
        var filters = new HashSet<Filter<Person>>
        {
            new Filter<Person>("Age", Criteria.HIGHER, 30)
        };

        // Act
        var result = _service.Match(filters);

        // Assert
        Assert.AreEqual(1, result.Count);
        Assert.AreEqual("Alice Johnson", result.First().Name);
    }

    [TestMethod]
    public void MatchQueryable_ShouldReturnCorrectResults_WithMultipleFilters()
    {
        // Arrange
        var filters = new HashSet<Filter<Person>>
        {
            new Filter<Person>("Age", Criteria.HIGHEREQUALS, 25),
            new Filter<Person>("Name", Criteria.CONTAINS, "Jane")
        };

        // Act
        var result = _service.Match(filters);

        // Assert
        Assert.AreEqual(1, result.Count);
        Assert.AreEqual("Jane Smith", result.First().Name);
    }

    [TestMethod]
    public void MatchQueryable_ShouldReturnZeroResults_WithMultipleFilters()
    {
        // Arrange
        var filters = new HashSet<Filter<Person>>
        {
            new Filter<Person>("Age", Criteria.HIGHER, 25),
            new Filter<Person>("Name", Criteria.CONTAINS, "Jane")
        };

        // Act
        var result = _service.Match(filters);

        // Assert
        Assert.AreEqual(0, result.Count);
    }

    [TestMethod]
    public void MatchQueryable_ShouldReturnCorrectResults_WithDateFilter()
    {
        // Arrange
        var filters = new HashSet<Filter<Person>>
        {
            new Filter<Person>("BirthDate", Criteria.LOWEREQUALS, new DateTime(1990, 1, 1))
        };

        // Act
        var result = _service.Match(filters);

        // Assert
        Assert.AreEqual(2, result.Count);
        Assert.AreEqual("John Doe", result.First().Name);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void MatchQueryable_ShouldThrowException_WithInvalidContainsFilter()
    {
        // Arrange
        var filters = new HashSet<Filter<Person>>
        {
            new Filter<Person>("Age", Criteria.CONTAINS, 30)
        };

        // Act
        _service.Match(filters);
    }

    [TestMethod]
    public void MatchQueryable_ShouldReturnEmpty_WhenNoMatchFound()
    {
        // Arrange
        var filters = new HashSet<Filter<Person>>
        {
            new Filter<Person>("Age", Criteria.HIGHER, 40)
        };

        // Act
        var result = _service.Match(filters);

        // Assert
        Assert.AreEqual(0, result.Count);
    }
}
