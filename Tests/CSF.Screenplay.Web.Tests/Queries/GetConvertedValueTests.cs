using System;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Actions;
using CSF.Screenplay.Web.Queries;
using CSF.Screenplay.Web.Tests.Pages;
using NUnit.Framework;
using static CSF.Screenplay.StepComposer;

namespace CSF.Screenplay.Web.Tests.Queries
{
  [TestFixture]
  public class GetConvertedValueTests
  {
    Actor joe;

    [SetUp]
    public void Setup()
    {
      joe = new Actor("joe");
      var browseTheWeb = WebdriverTestSetup.GetDefaultWebBrowsingAbility();
      joe.IsAbleTo(browseTheWeb);
    }

    [Test]
    public void GetValue_returns_expected_value()
    {
      // Arrange
      var homePage = new HomePage();
      var openTheHomePage = new Open(homePage);
      var readTheValue = new GetConvertedValue<int>(homePage.ImportantNumber);

      Given(joe).WasAbleTo(openTheHomePage);

      // Act
      var result = When(joe).AttemptsTo(readTheValue);

      // Assert
      Assert.AreEqual(42, result);
    }
  }
}
