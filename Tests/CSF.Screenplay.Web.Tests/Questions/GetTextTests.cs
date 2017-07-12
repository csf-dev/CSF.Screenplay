using System;
using CSF.Screenplay.Web.Builders;
using CSF.Screenplay.Web.Tests.Pages;
using NUnit.Framework;
using FluentAssertions;
using static CSF.Screenplay.StepComposer;

namespace CSF.Screenplay.Web.Tests.Questions
{
  [TestFixture]
  public class GetTextTests
  {
    Actor joe;

    [SetUp]
    public void Setup()
    {
      joe = WebdriverTestSetup.GetJoe();
    }

    [Test]
    public void GetText_returns_expected_value()
    {
      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<HomePage>());

      var result = When(joe).Sees(TheText.Of(HomePage.ImportantString));

      WebdriverTestSetup.TakeScreenshot(GetType(), nameof(GetText_returns_expected_value));
      result.Should().Be("banana!");
    }
  }
}
