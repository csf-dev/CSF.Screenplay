using NUnit.Framework;
using System;
using CSF.Screenplay.Selenium.Abilities;
using Moq;
using OpenQA.Selenium;
using CSF.Screenplay.Abilities;

namespace CSF.Screenplay.Selenium.Tests.Abilities
{
  [TestFixture]
  public class BrowseTheWebTests
  {
    [Test]
    public void Dispose_should_never_dispose_the_web_driver()
    {
      // Arrange
      var webDriver = Mock.Of<IWebDriver>();
      IAbility sut = new BrowseTheWeb(webDriver);

      // Act
      sut.Dispose();

      // Assert
      Mock.Get(webDriver).Verify(x => x.Dispose(), Times.Never());
    }
  }
}
