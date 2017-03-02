namespace Autotask.Tests
{
    using System.Xml.Linq;
    using Shouldly;
    using Xunit;

    public class Query_Tests
    {
        [Fact]
        public void Check_Entity_Element()
        {
            // Arrange
            string stringQuery = "Contract id GreaterThan 0";

            // Act
            var output = Query.Generate(stringQuery);

            // Assert
            output.Root.Element("entity").ShouldNotBeNull();
            output.Root.Element("entity").Value.ShouldBe("Contract");
        }

        [Fact]
        public void Check_Query_Element()
        {
            // Arrange
            string stringQuery = "Contract id GreaterThan 0";

            // Act
            var output = Query.Generate(stringQuery);

            // Assert
            output.Root.Element("query").ShouldNotBeNull();
            output.Root.Element("query").Element("field").ShouldNotBeNull();
            var t = output.Root.Element("query").Element("field").Value;
            output.Root.Element("query").Element("field").Element("expression").ShouldNotBeNull();
        }

        [Fact]
        public void Should_Be_XDocument()
        {
            // Arrange
            string stringQuery = "Contract id GreaterThan 0";

            // Act
            var output = Query.Generate(stringQuery);

            // Assert
            output.ShouldBeOfType<XDocument>();
        }

        [Fact]
        public void Should_Not_Be_Null()
        {
            // Arrange
            string query = "Contract id GreaterThan 0";

            // Act
            var output = Query.Generate(query);

            // Assert
            output.ShouldNotBeNull();
        }
    }
}