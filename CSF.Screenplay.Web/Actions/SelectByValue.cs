using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;

namespace CSF.Screenplay.Web.Actions
{
  public class SelectByValue : SelectAction
  {
    readonly string value;

    protected override string GetReport(INamed actor)
    {
      return $"{actor.Name} selects option <{value}> from {GetTargetName()}.";
    }

    protected override void PerformAs(IPerformer actor, BrowseTheWeb ability, IWebElement element, SelectElement select)
    {
      select.SelectByValue(value);
    }

    public SelectByValue(ITarget target, string value) : base(target)
    {
      if(value == null)
        throw new ArgumentNullException(nameof(value));

      this.value = value;
    }

    public SelectByValue(IWebElement element, string value) : base(element)
    {
      if(value == null)
        throw new ArgumentNullException(nameof(value));

      this.value = value;
    }
  }
}
