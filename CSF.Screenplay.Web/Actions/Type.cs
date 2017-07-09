using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Actions
{
  public class Type : TargettedAction
  {
    readonly string text;

    protected override string GetReport(INamed actor)
    {
      return $"{actor.Name} types '{text}' into {GetTargetName()}";
    }

    protected override void PerformAs(IPerformer actor, BrowseTheWeb ability, IWebElement element)
    {
      element.SendKeys(text);
    }

    public Type(ITarget target, string text) : base(target)
    {
      if(text == null)
        throw new ArgumentNullException(nameof(text));
      
      this.text = text;
    }

    public Type(IWebElement element, string text) : base(element)
    {
      if(text == null)
        throw new ArgumentNullException(nameof(text));

      this.text = text;
    }
  }
}
