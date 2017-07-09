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
    readonly static WebElementProvider elementProvider;
    readonly ITarget target;
    readonly IWebElement element;

    internal static WebElementProvider ElementProvider => elementProvider;

    protected override void PerformAs(IPerformer actor)
    {
      var ability = GetAbility(actor);
      var ele = GetWebElement(ability);
      PerformAs(actor, ability, ele);
    }

    protected virtual BrowseTheWeb GetAbility(IPerformer actor)
    {
      return actor.GetAbility<BrowseTheWeb>();
    }

    protected virtual IWebElement GetWebElement(BrowseTheWeb ability)
    {
      if(element != null)
        return element;

      return elementProvider.GetElement(ability, target);
    }

    protected virtual string GetTargetName()
    {
      if(target != null)
        return target.GetName();

      return $"the <{element.TagName}> element";
    }

    protected abstract void PerformAs(IPerformer actor, BrowseTheWeb ability, IWebElement element);

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

    static TargettedAction()
    {
      elementProvider = new WebElementProvider();
    }
  }
}
