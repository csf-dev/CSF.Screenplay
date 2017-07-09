using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CSF.Screenplay.Web.Actions
{
  public abstract class SelectAction : TargettedAction
  {
    protected override void PerformAs(IPerformer actor, BrowseTheWeb ability, IWebElement element)
    {
      var selectElement = new SelectElement(element);
      PerformAs(actor, ability, element, selectElement);
    }

    protected abstract void PerformAs(IPerformer actor, BrowseTheWeb ability, IWebElement element, SelectElement select);

    public SelectAction(ITarget target) : base(target) {}

    public SelectAction(IWebElement element) : base(element) {}
  }
}
