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
  [Description("Reading the value of a form element")]
  public class GetValueTests
  {
    [Test]
    [Description("Reading the value of an element detects the expected value.")]
    public void GetValue_returns_expected_value()
    {
      var joe = Stage.Cast.GetOrCreate("joe");

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      Then(joe).ShouldSee(TheValue.Of(PageTwo.SecondTextbox)).Should().Be("This is a text box");
    }

    [Test]
    [Description("Reading the value of an element and converting it to a number detects the expected value.")]
    public void GetConvertedValue_returns_expected_value()
    {
      var joe = Stage.Cast.GetOrCreate("joe");

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());
      Given(joe).WasAbleTo(Enter.TheText("55").Into(PageTwo.SpecialInputField));

      Then(joe).ShouldSee(TheValue.From(PageTwo.SpecialInputField).As<int>()).Should().Be(55);
    }
  }
}
