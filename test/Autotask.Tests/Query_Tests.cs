namespace Autotask.Tests
{
    using System.Xml.Linq;
    using Operations;
    using Shouldly;
    using Xunit;

    public class Query_Tests
    {
        [Fact]
        public void Check_Entity_Element()
        {
            // Arrange
            const string stringQuery = "Contract id GreaterThan 0";

            // Act
            XDocument output = Query.Generate(stringQuery);

            // Assert
            const string expectedOutput = @"<queryxml version=""1.0"">
  <entity>Contract</entity>
  <query>
    <field>id<expression op=""GreaterThan"">0</expression></field>
  </query>
</queryxml>";

            output.ToString().ShouldBe(expectedOutput);
        }

        [Fact]
        public void Check_Query_Element()
        {
            // Arrange
            const string stringQuery = "Contract id GreaterThan 0";

            // Act
            XDocument output = Query.Generate(stringQuery);

            // Assert
            const string expectedOutput = @"<queryxml version=""1.0"">
  <entity>Contract</entity>
  <query>
    <field>id<expression op=""GreaterThan"">0</expression></field>
  </query>
</queryxml>";

            output.ToString().ShouldBe(expectedOutput);
        }

        [Fact]
        public void Should_Be_XDocument()
        {
            // Arrange
            const string stringQuery = "Contract id GreaterThan 0";

            // Act
            XDocument output = Query.Generate(stringQuery);

            // Assert
            output.ShouldNotBeNull();
            output.ShouldBeOfType<XDocument>();
        }
    }
}