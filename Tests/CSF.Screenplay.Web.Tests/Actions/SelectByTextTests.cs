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
  public class SelectByTextTests
  {
    Actor joe;

    [SetUp]
    public void Setup()
    {
      joe = WebdriverTestSetup.GetJoe();
    }

    [Test]
    public void SelectByText_generates_expected_result_on_page()
    {
      // Arrange
      var pageTwo = new PageTwo();
      var openPageTwo = new Open(pageTwo);
      var selectTheValue = new SelectByText(pageTwo.SingleSelectionList, "Two");
      var seeTheValue = new GetConvertedValue<int>(pageTwo.SingleSelectionValue);

      Given(joe).WasAbleTo(openPageTwo);

      // Act
      When(joe).AttemptsTo(selectTheValue);

      // Assert
      WebdriverTestSetup.TakeScreenshot(GetType(), nameof(SelectByText_generates_expected_result_on_page));
      var result = Then(joe).Should(seeTheValue);
      Assert.AreEqual(2, result);
    }
  }
}
