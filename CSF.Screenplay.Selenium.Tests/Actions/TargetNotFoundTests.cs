﻿using System;
using CSF.Screenplay.Web.Builders;
using CSF.Screenplay.Web.Models;
using CSF.Screenplay.Web.Tests.Pages;
using CSF.Screenplay.NUnit;
using NUnit.Framework;
using static CSF.Screenplay.StepComposer;

namespace CSF.Screenplay.Web.Tests.Actions
{
  [TestFixture]
  [Description("Behaviours when a target is not found for a desired action")]
  public class TargetNotFoundTests
  {
    [Test,Screenplay]
    [Description("Attempting to click on a link which does not exist raises an appropriate 'target not found' exception.")]
    public void Click_on_non_existent_element_raises_TargetNotFoundException(IScreenplayScenario scenario)
    {
      var joe = scenario.GetJoe();

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<HomePage>());

      Assert.That(() => When(joe).AttemptsTo(Click.On(PageTwo.ListOfItems)),
                  Throws.TypeOf<TargetNotFoundException>());
    }

    [Test,Screenplay]
    [Description("When a 'target not found' exception is raised, it should have a name which matches the missing target.")]
    public void TargetNotFoundException_raised_has_correct_target_name(IScreenplayScenario scenario)
    {
      var joe = scenario.GetJoe();

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<HomePage>());

      IHasTargetName target = null;
      try
      {
        When(joe).AttemptsTo(Click.On(PageTwo.ListOfItems));
        Assert.Fail("The action should raise an exception.");
      }
      catch(TargetNotFoundException ex)
      {
        target = ex.Target;
      }
      catch(Exception ex)
      {
        Assert.Fail($"Wrong exception type caught. Expected {nameof(TargetNotFoundException)}, got:\n{ex.ToString()}");
      }

      Assert.That(target, Is.Not.Null, "Target should not be null.");
      Assert.That(target.GetName(), Is.EqualTo(PageTwo.ListOfItems.GetName()), "Target has the correct name");
    }

    [Test,Screenplay]
    [Description("A 'target not found' exception should include the target name in its report when the target is provided.")]
    public void TargetNotFoundException_includes_target_name_in_report(IScreenplayScenario scenario)
    {
      var joe = scenario.GetJoe();
      var ex = new TargetNotFoundException() { Target = PageTwo.ListOfItems };

      var result = ex.GetReport(joe);

      Assert.That(result, Is.EqualTo("Joe cannot see the list of items on the screen."));
    }

    [Test,Screenplay]
    [Description("A 'target not found' exception should include the target name in its report when the target is provided.")]
    public void TargetNotFoundException_can_create_a_report_without_target(IScreenplayScenario scenario)
    {
      var joe = scenario.GetJoe();
      var ex = new TargetNotFoundException();

      var result = ex.GetReport(joe);

      Assert.That(result, Is.EqualTo("Joe cannot see the required element on the screen."));
    }
  }
}
