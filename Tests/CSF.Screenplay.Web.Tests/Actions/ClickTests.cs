using System;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Actions;
using CSF.Screenplay.Web.Queries;
using CSF.Screenplay.Web.Tests.Pages;
using NUnit.Framework;
using static CSF.Screenplay.StepComposer;

namespace CSF.Screenplay.Web.Tests.Actions
{
  [TestFixture]
  public class ClickTests
  {
    Actor joe;

    [SetUp]
    public void Setup()
    {
      joe = WebdriverTestSetup.GetJoe();
    }

    [Test]
    public void Click_OnLinkToPageTwo_navigates_to_second_page()
    {
      // Arrange
      var homePage = new HomePage();
      var openTheHomePage = new Open(homePage);
      var clickTheLinkToTheSecondPage = new Click(homePage.SecondPageLink);

      Given(joe).WasAbleTo(openTheHomePage);

      // Act
      When(joe).AttemptsTo(clickTheLinkToTheSecondPage);

      // Assert
      WebdriverTestSetup.TakeScreenshot(GetType(), nameof(Click_OnLinkToPageTwo_navigates_to_second_page));
      var result = Then(joe).Should(new GetWindowTitle());
      Assert.AreEqual("Page two", result);
    }
  }
}
