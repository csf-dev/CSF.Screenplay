using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Actions;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Questions
{
  public abstract class TargettedQuestion<T> : Question<T>
  {
    readonly ITarget target;
    IWebElement element;

    protected override T GetAnswer(IPerformer actor)
    {
      var ability = GetAbility(actor);
      var ele = GetWebElement(ability);
      var adapter = GetWebElementAdapter(ele);
      return PerformAs(actor, ability, adapter);
    }

    protected virtual BrowseTheWeb GetAbility(IPerformer actor)
    {
      return actor.GetAbility<BrowseTheWeb>();
    }

    protected virtual IWebElement GetWebElement(BrowseTheWeb ability)
    {
      if(element != null)
        return element;

      element = WebElementProvider.Instance.GetElement(ability, target);
      return element;
    }

    protected virtual IWebElementAdapter GetWebElementAdapter(IWebElement element)
    {
      return new WebElementAdapter(element);
    }

    protected virtual string GetTargetName()
    {
      if(target != null)
        return target.GetName();

      if(element != null)
        return $"the <{element.TagName}> element";

      return "the element";
    }

    protected abstract T PerformAs(IPerformer actor, BrowseTheWeb ability, IWebElementAdapter adapter);

    public TargettedQuestion(ITarget target)
    {
      if(target == null)
        throw new ArgumentNullException(nameof(target));

      this.target = target;
    }

    public TargettedQuestion(IWebElement element)
    {
      if(element == null)
        throw new ArgumentNullException(nameof(element));

      this.element = element;
    }
  }
}
