using System;
using CSF.Screenplay.Web.Builders;
using CSF.Screenplay.Web.Tests.Pages;
using NUnit.Framework;
using FluentAssertions;
using static CSF.Screenplay.StepComposer;

namespace CSF.Screenplay.Web.Tests.Questions
{
  [TestFixture]
  public class GetValueTests
  {
    Actor joe;

    [SetUp]
    public void Setup()
    {
      joe = WebdriverTestSetup.GetJoe();
    }

    [Test,Reportable]
    public void GetValue_returns_expected_value()
    {
      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      Then(joe).ShouldSee(TheValue.Of(PageTwo.SecondTextbox)).Should().Be("This is a text box");
    }

    [Test,Reportable]
    public void GetConvertedValue_returns_expected_value()
    {
      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());
      Given(joe).WasAbleTo(Enter.TheText("55").Into(PageTwo.SpecialInputField));

      Then(joe).ShouldSee(TheValue.From(PageTwo.SpecialInputField).As<int>()).Should().Be(55);
    }
  }
}
