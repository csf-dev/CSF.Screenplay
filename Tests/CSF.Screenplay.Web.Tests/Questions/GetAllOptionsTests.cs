using System;
using CSF.Screenplay.Web.Builders;
using CSF.Screenplay.Web.Tests.Pages;
using NUnit.Framework;
using FluentAssertions;
using static CSF.Screenplay.StepComposer;

namespace CSF.Screenplay.Web.Tests.Questions
{
  [TestFixture]
  public class GetAllOptionsTests
  {
    Actor joe;

    [SetUp]
    public void Setup()
    {
      joe = WebdriverTestSetup.GetJoe();
    }

    [Test]
    public void GetAllOptions_returns_expected_collection()
    {
      var expected = new Models.Option[] {
        new Models.Option("One", "1"),
        new Models.Option("Two", "2"),
        new Models.Option("Three", "3"),
      };

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      var result = When(joe).Sees(TheOptions.In(PageTwo.SingleSelectionList));

      WebdriverTestSetup.TakeScreenshot(GetType(), nameof(GetAllOptions_returns_expected_collection));
      result.ShouldBeEquivalentTo(expected);
    }
  }
}
