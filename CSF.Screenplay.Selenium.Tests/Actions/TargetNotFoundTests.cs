using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.NUnit;
using CSF.Screenplay.Selenium.Abilities;
using CSF.Screenplay.Selenium.Builders;
using CSF.Screenplay.Selenium.Models;
using CSF.Screenplay.Selenium.Tests.Pages;
using NUnit.Framework;
using static CSF.Screenplay.StepComposer;

namespace CSF.Screenplay.Selenium.Tests.Actions
{
  [TestFixture]
  [Description("Behaviours when a target is not found for a desired action")]
  public class TargetNotFoundTests
  {
    [Test,Screenplay]
    [Description("Attempting to click on a link which does not exist raises an appropriate 'target not found' exception.")]
    public void Click_on_non_existent_element_raises_TargetNotFoundException(ICast cast, Lazy<BrowseTheWeb> webBrowserFactory)
    {
      var joe = cast.GetJoe(webBrowserFactory);

      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<HomePage>());

      Assert.That(() => When(joe).AttemptsTo(Click.On(PageTwo.ListOfItems)),
                  Throws.TypeOf<TargetNotFoundException>());
    }

    [Test,Screenplay]
    [Description("When a 'target not found' exception is raised, it should have a name which matches the missing target.")]
    public void TargetNotFoundException_raised_has_correct_target_name(ICast cast, Lazy<BrowseTheWeb> webBrowserFactory)
    {
      var joe = cast.GetJoe(webBrowserFactory);

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
    public void TargetNotFoundException_includes_target_name_in_report(ICast cast, Lazy<BrowseTheWeb> webBrowserFactory)
    {
      var joe = cast.GetJoe(webBrowserFactory);
      var ex = new TargetNotFoundException() { Target = PageTwo.ListOfItems };

      var result = ex.GetReport(joe);

      Assert.That(result, Is.EqualTo("Joe cannot see the list of items on the screen."));
    }

    [Test,Screenplay]
    [Description("A 'target not found' exception should include the target name in its report when the target is provided.")]
    public void TargetNotFoundException_can_create_a_report_without_target(ICast cast, Lazy<BrowseTheWeb> webBrowserFactory)
    {
      var joe = cast.GetJoe(webBrowserFactory);
      var ex = new TargetNotFoundException();

      var result = ex.GetReport(joe);

      Assert.That(result, Is.EqualTo("Joe cannot see the required element on the screen."));
    }
  }
}
