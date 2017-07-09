using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Actions
{
  public abstract class TargettedAction<T> : Performable<T>
  {
    readonly ITarget target;
    readonly IWebElement element;

    protected ITarget Target => target;
    protected IWebElement Element => element;

    protected override T PerformAs(IPerformer actor)
    {
      var ability = GetAbility(actor);
      var ele = GetWebElement(ability);
      return PerformAs(actor, ability, ele);
    }

    protected virtual BrowseTheWeb GetAbility(IPerformer actor)
    {
      return actor.GetAbility<BrowseTheWeb>();
    }

    protected virtual IWebElement GetWebElement(BrowseTheWeb ability)
    {
      if(Element != null)
        return element;

      return TargettedAction.ElementProvider.GetElement(ability, Target);
    }

    protected abstract T PerformAs(IPerformer actor, BrowseTheWeb ability, IWebElement element);

    public TargettedAction(ITarget target)
    {
      if(target == null)
        throw new ArgumentNullException(nameof(target));

      this.target = target;
    }

    public TargettedAction(IWebElement element)
    {
      if(element == null)
        throw new ArgumentNullException(nameof(element));

      this.element = element;
    }
  }
}
