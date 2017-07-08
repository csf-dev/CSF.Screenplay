using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Actions
{
  public abstract class TargettedAction : Performable
  {
    readonly ITarget target;

    protected ITarget Target => target;

    protected override void PerformAs(IPerformer actor)
    {
      var ability = GetAbility(actor);
      var elementLocator = Target.GetWebDriverLocator();
      var element = GetWebElement(ability, elementLocator);
      PerformAs(actor, ability, element);
    }

    protected virtual BrowseTheWeb GetAbility(IPerformer actor)
    {
      return actor.GetAbility<BrowseTheWeb>();
    }

    protected virtual IWebElement GetWebElement(BrowseTheWeb ability, By elementLocator)
    {
      return ability.WebDriver.FindElement(elementLocator);
    }

    protected abstract void PerformAs(IPerformer actor, BrowseTheWeb ability, IWebElement element);

    public TargettedAction(ITarget target)
    {
      if(target == null)
        throw new ArgumentNullException(nameof(target));

      this.target = target;
    }
  }
}
