using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Actions;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Queries
{
  public class GetValue : TargettedAction<string>
  {
    protected override string GetReport(INamed actor)
    {
      return $"{actor.Name} reads {Target.GetName()}.";
    }

    protected override string PerformAs(IPerformer actor, BrowseTheWeb ability, IWebElement element)
    {
      return element.Text;
    }

    public GetValue(ITarget target) : base(target) {}

    public GetValue(IWebElement element) : base(element) {}
  }
}
