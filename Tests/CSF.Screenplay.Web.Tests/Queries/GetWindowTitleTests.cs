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
  public class GetWindowTitleTests
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
    public void GetWindowTitle_returns_correct_result()
    {
      // Arrange
      var homePage = new HomePage();
      var openTheHomePage = new Open(homePage);
      var readTheWindowTitle = new GetWindowTitle();

      Given(joe).WasAbleTo(openTheHomePage);

      // Act
      var result = When(joe).AttemptsTo(readTheWindowTitle);

      // Assert
      Assert.AreEqual("App home page", result);
    }
  }
}
