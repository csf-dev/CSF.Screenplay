using System;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Actions;
using CSF.Screenplay.Web.Questions;
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
      joe = WebdriverTestSetup.GetJoe();
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
      WebdriverTestSetup.TakeScreenshot(GetType(), nameof(GetWindowTitle_returns_correct_result));
      Assert.AreEqual("App home page", result);
    }
  }
}
