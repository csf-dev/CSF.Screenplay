using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Questions
{
  public class GetText : TargettedQuestion<string>
  {
    protected override string GetReport(INamed actor)
    {
      return $"{actor.Name} reads {GetTargetName()}.";
    }

    protected override string PerformAs(IPerformer actor, BrowseTheWeb ability, IWebElementAdapter adapter)
    {
      return adapter.GetText();
    }

    public GetText(ITarget target) : base(target) {}

    public GetText(IWebElement element) : base(element) {}
  }
}
