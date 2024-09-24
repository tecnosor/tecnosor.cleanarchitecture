using tecnosor.cleanarchitecture.common.domain.match;

namespace tests.unittests.tecnosor.cleanarchitecture.common.domain.match;

[TestClass]
public class FilterTests
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Info Info { get; set; }
    }

    public class Info
    {
        public DateTime Nacimiento { get; set; }
    }

    [TestMethod]
    public void Filter_ValidParameters_ShouldCreateFilter()
    {
        // Arrange
        var filter = new Filter<Person>("Age", Criteria.HIGHER, 30);

        // Act & Assert
        Assert.AreEqual("Age", filter.Field);
        Assert.AreEqual(Criteria.HIGHER, filter.Operation);
        Assert.AreEqual(30, filter.Value);
    }

    [TestMethod]
    public void Filter_InvalidType_ShouldThrowException()
    {
        // Arrange
        try
        {
            // Act
            var filter = new Filter<Person>("Age", Criteria.HIGHER, "30");
            Assert.Fail("Expected exception was not thrown");
        }
        catch (ArgumentException ex)
        {
            // Assert
            Assert.AreEqual("value of Age is not the same type than the comparable value given", ex.Message);
        }
    }

    [TestMethod]
    public void Filter_ValidNestedProperty_ShouldCreateFilter()
    {
        // Arrange
        var filter = new Filter<Person>("Info.Nacimiento", Criteria.HIGHER, new DateTime(2000, 1, 1));

        // Act & Assert
        Assert.AreEqual("Info.Nacimiento", filter.Field);
        Assert.AreEqual(Criteria.HIGHER, filter.Operation);
        Assert.AreEqual(new DateTime(2000, 1, 1), filter.Value);
    }

    [TestMethod]
    public void Filter_InvalidNestedProperty_ShouldThrowException()
    {
        // Arrange
        try
        {
            // Act
            var filter = new Filter<Person>("Info.InvalidField", Criteria.HIGHER, new DateTime(2000, 1, 1));
            Assert.Fail("Expected exception was not thrown");
        }
        catch (ArgumentException ex)
        {
            // Assert
            StringAssert.Contains(ex.Message, "Field 'InvalidField' not found");
        }
    }

    [TestMethod]
    public void Filter_InvalidField_ShouldThrowException()
    {
        // Arrange
        try
        {
            // Act
            var filter = new Filter<Person>("InvalidField", Criteria.HIGHER, 30);
            Assert.Fail("Expected exception was not thrown");
        }
        catch (ArgumentException ex)
        {
            // Assert
            StringAssert.Contains(ex.Message, "Field 'InvalidField' not found");
        }
    }
}
