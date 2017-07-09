using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;

namespace CSF.Screenplay.Web.Actions
{
  public class DeselectAll : SelectAction
  {
    protected override string GetReport(INamed actor)
    {
      return $"{actor.Name} deselects all options in {GetTargetName()}.";
    }

    protected override void PerformAs(IPerformer actor, BrowseTheWeb ability, IWebElement element, SelectElement select)
    {
      select.DeselectAll();
    }

    public DeselectAll(ITarget target) : base(target) {}

    public DeselectAll(IWebElement element) : base(element) {}
  }
}
