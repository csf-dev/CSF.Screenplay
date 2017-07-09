using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Actions;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Queries
{
  public abstract class TargettedQuery<T> : Performable<T>
  {
    readonly ITarget target;
    readonly IWebElement element;

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
      if(element != null)
        return element;

      return TargettedAction.ElementProvider.GetElement(ability, target);
    }

    protected virtual string GetTargetName()
    {
      if(target != null)
        return target.GetName();

      return $"the <{element.TagName}> element";
    }

    protected abstract T PerformAs(IPerformer actor, BrowseTheWeb ability, IWebElement element);

    public TargettedQuery(ITarget target)
    {
      if(target == null)
        throw new ArgumentNullException(nameof(target));

      this.target = target;
    }

    public TargettedQuery(IWebElement element)
    {
      if(element == null)
        throw new ArgumentNullException(nameof(element));

      this.element = element;
    }
  }
}
