namespace Autotask.Tests
{
    using System.Linq;
    using System.Xml.Linq;
    using Operations;
    using Shouldly;
    using Xunit;

    public class Query_Tests
    {
        [Fact]
        public void Generate_GreaterThan_ShouldOutputCorrectXml()
        {
            // Arrange
            const string stringQuery = "Contract id GreaterThan 0";

            // Act
            XDocument output = Query.Generate(stringQuery);

            // Assert
            var expectedOutput = XDocument.Parse("<queryxml version=\"1.0\">\r\n  <entity>Contract</entity>\r\n  <query>\r\n    <field>id<expression op=\"GreaterThan\">0</expression></field>\r\n  </query>\r\n</queryxml>");

            Normalize(output).ShouldBe(Normalize(expectedOutput));
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

        private static string Normalize(XDocument source)
        {
            var sourceWithoutLineBreaks = source.ToString().Replace("\r\n", string.Empty).Replace("\r", string.Empty).Replace("\n", string.Empty);
            return new string(sourceWithoutLineBreaks.ToCharArray()
                .Where(c => !char.IsWhiteSpace(c))
                .ToArray());
        }
    }
}