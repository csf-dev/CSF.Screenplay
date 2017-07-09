using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;

namespace CSF.Screenplay.Web.Actions
{
  public class SelectByText : SelectAction
  {
    readonly string text;

    protected override string GetReport(INamed actor)
    {
      return $"{actor.Name} selects '{text}' from {GetTargetName()}.";
    }

    protected override void PerformAs(IPerformer actor, BrowseTheWeb ability, IWebElement element, SelectElement select)
    {
      select.SelectByText(text);
    }

    public SelectByText(ITarget target, string text) : base(target)
    {
      if(text == null)
        throw new ArgumentNullException(nameof(text));

      this.text = text;
    }

    public SelectByText(IWebElement element, string text) : base(element)
    {
      if(text == null)
        throw new ArgumentNullException(nameof(text));

      this.text = text;
    }
  }
}
