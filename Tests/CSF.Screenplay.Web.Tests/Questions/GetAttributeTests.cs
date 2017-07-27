using System;
using CSF.Screenplay.Web.Builders;
using CSF.Screenplay.Web.Tests.Pages;
using NUnit.Framework;
using FluentAssertions;
using static CSF.Screenplay.StepComposer;
using CSF.Screenplay.NUnit;

namespace CSF.Screenplay.Web.Tests.Questions
{
  [TestFixture]
  [Description("Reading element attributes")]
  public class GetAttributeTests
  {
    [Test]
    [Description("Reading the value of a 'title' attribute detects the expected value.")]
    public void GetAttribute_returns_expected_value()
    {
      var joe = Stage.Cast.GetOrAdd("joe");

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      Then(joe).ShouldSee(TheAttribute.Named("title").From(PageTwo.TheDynamicTextArea)).Should().Be("This is a dynamic value");
    }
  }
}
