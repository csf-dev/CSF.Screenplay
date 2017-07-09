using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;

namespace CSF.Screenplay.Web.Actions
{
  public class SelectByIndex : SelectAction
  {
    readonly int index;

    protected override string GetReport(INamed actor)
    {
      return $"{actor.Name} selects option {index + 1} from {GetTargetName()}.";
    }

    protected override void PerformAs(IPerformer actor, BrowseTheWeb ability, IWebElement element, SelectElement select)
    {
      select.SelectByIndex(index);
    }

    public SelectByIndex(ITarget target, int index) : base(target)
    {
      this.index = index;
    }

    public SelectByIndex(IWebElement element, int index) : base(element)
    {
      this.index = index;
    }
  }
}
