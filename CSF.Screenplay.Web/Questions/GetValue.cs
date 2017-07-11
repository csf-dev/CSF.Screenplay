using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Questions
{
  public class GetValue : TargettedQuestion<string>
  {
    protected override string GetReport(INamed actor)
    {
      return $"{actor.Name} reads the value of {GetTargetName()}.";
    }

    protected override string PerformAs(IPerformer actor, BrowseTheWeb ability, IWebElementAdapter adapter)
    {
      return adapter.GetValue();
    }

    public GetValue(ITarget target) : base(target) {}

    public GetValue(IWebElement element) : base(element) {}
  }
}
