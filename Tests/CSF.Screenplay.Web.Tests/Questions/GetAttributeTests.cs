using System;
using CSF.Screenplay.Web.Builders;
using CSF.Screenplay.Web.Tests.Pages;
using NUnit.Framework;
using FluentAssertions;
using static CSF.Screenplay.StepComposer;

namespace CSF.Screenplay.Web.Tests.Questions
{
  [TestFixture]
  public class GetAttributeTests
  {
    Actor joe;

    [SetUp]
    public void Setup()
    {
      joe = WebdriverTestSetup.GetJoe();
    }

    [Test]
    public void GetAttribute_returns_expected_value()
    {
      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      var result = When(joe).Sees(TheAttribute.Named("title").From(PageTwo.TheDynamicTextArea));

      WebdriverTestSetup.TakeScreenshot(GetType(), nameof(GetAttribute_returns_expected_value));
      result.ShouldBeEquivalentTo("This is a dynamic value");
    }
  }
}
