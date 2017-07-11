using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Questions
{
  public class GetLocation : TargettedQuestion<Position>
  {
    protected override string GetReport(INamed actor)
    {
      return $"{actor.Name} gets the screen location of {GetTargetName()}.";
    }

    protected override Position PerformAs(IPerformer actor, BrowseTheWeb ability, IWebElementAdapter adapter)
    {
      return adapter.GetLocation();
    }

    public GetLocation(ITarget target) : base(target) {}

    public GetLocation(IWebElement element) : base(element) {}
  }
}
