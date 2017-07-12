using System;
using CSF.Screenplay.Web.Builders;
using CSF.Screenplay.Web.Tests.Pages;
using NUnit.Framework;
using FluentAssertions;
using static CSF.Screenplay.StepComposer;

namespace CSF.Screenplay.Web.Tests.Questions
{
  [TestFixture]
  public class GetConvertedTextTests
  {
    Actor joe;

    [SetUp]
    public void Setup()
    {
      joe = WebdriverTestSetup.GetJoe();
    }

    [Test]
    public void GetConvertedText_returns_expected_value()
    {
      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<HomePage>());

      var result = When(joe).Sees(TheText.From(HomePage.ImportantNumber).As<int>());

      WebdriverTestSetup.TakeScreenshot(GetType(), nameof(GetConvertedText_returns_expected_value));
      result.Should().Be(42);
    }
  }
}
