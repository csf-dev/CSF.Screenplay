using System.IO;
using System.Linq;
using System.Reflection;
using CSF.WebDriverExtras.Flags;
using Moq;
using NUnit.Framework;

namespace CSF.Screenplay.Selenium.Tests
{
  [TestFixture,Parallelizable(ParallelScope.All)]
  public class GetBrowserFlagsDefinitionsTests
  {
    [Test,AutoMoqData]
    public void GetDefinitions_from_test_assembly_returns_one_definition(IReadsFlagsDefinitions definitionReader,
                                                                         FlagsDefinition def)
    {
      // Arrange
      var testAssembly = Assembly.GetExecutingAssembly();
      Mock.Get(definitionReader)
          .Setup(x => x.GetFlagsDefinitions(It.IsAny<Stream>()))
          .Returns(new [] { def });
      var sut = new GetBrowserFlagsDefinitions(definitionReader);

      // Act
      var result = sut.GetDefinitions(testAssembly);

      // Assert
      Assert.That(result, Has.Length.EqualTo(1));
    }

    [Test,AutoMoqData]
    public void GetDefinitions_from_test_assembly_uses_definition_reader(IReadsFlagsDefinitions definitionReader,
                                                                         FlagsDefinition def)
    {
      // Arrange
      var testAssembly = Assembly.GetExecutingAssembly();
      Mock.Get(definitionReader)
          .Setup(x => x.GetFlagsDefinitions(It.IsAny<Stream>()))
          .Returns(new [] { def });
      var sut = new GetBrowserFlagsDefinitions(definitionReader);

      // Act
      var result = sut.GetDefinitions(testAssembly);

      // Assert
      Mock.Get(definitionReader)
          .Verify(x => x.GetFlagsDefinitions(It.IsAny<Stream>()), Times.Once);
    }

    [Test,Category("Integration")]
    public void GetDefinitions_integration_test_only_finds_one_definition()
    {
      // Arrange
      var testAssembly = Assembly.GetExecutingAssembly();
      var sut = new GetBrowserFlagsDefinitions();

      // Act
      var result = sut.GetDefinitions(testAssembly);

      // Assert
      Assert.That(result, Has.Length.EqualTo(1));
    }

    [Test,Category("Integration")]
    public void GetDefinitions_integration_test_finds_definition_for_FooBrowser()
    {
      // Arrange
      var testAssembly = Assembly.GetExecutingAssembly();
      var sut = new GetBrowserFlagsDefinitions();

      // Act
      var result = sut.GetDefinitions(testAssembly).Single();

      // Assert
      Assert.That(result.BrowserNames.First(), Is.EqualTo("FooBrowser"));
    }

    [Test,Category("Integration")]
    public void FromDefinitionsAssembly_integration_test_returns_more_than_two_flags_definitions()
    {
      // Act
      var result = GetBrowserFlagsDefinitions.FromDefinitionsAssembly();

      // Assert
      Assert.That(result, Is.Not.Null);
      Assert.That(result, Has.Length.GreaterThan(2));
    }
  }
}
