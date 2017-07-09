using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;

namespace CSF.Screenplay.Web.Actions
{
  public class DeselectByIndex : SelectAction
  {
    readonly int index;

    protected override string GetReport(INamed actor)
    {
      return $"{actor.Name} deselects option {index + 1} from {GetTargetName()}.";
    }

    protected override void PerformAs(IPerformer actor, BrowseTheWeb ability, IWebElement element, SelectElement select)
    {
      select.DeselectByIndex(index);
    }

    public DeselectByIndex(ITarget target, int index) : base(target)
    {
      this.index = index;
    }

    public DeselectByIndex(IWebElement element, int index) : base(element)
    {
      this.index = index;
    }
  }
}
