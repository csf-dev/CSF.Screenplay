using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Actions
{
  public class Click : TargettedAction
  {
    protected override string GetReport(INamed actor)
    {
      return $"{actor.Name} clicks on {GetTargetName()}";
    }

    protected override void PerformAs(IPerformer actor, BrowseTheWeb ability, IWebElement element)
    {
      element.Click();
    }

    public Click(ITarget target) : base(target) {}

    public Click(IWebElement element) : base(element) {}
  }
}
