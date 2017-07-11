using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Questions
{
  public class GetConvertedValue<T> : TargettedQuestion<T>
  {
    protected override string GetReport(INamed actor)
    {
      return $"{actor.Name} reads the value of {GetTargetName()}.";
    }

    protected override T PerformAs(IPerformer actor, BrowseTheWeb ability, IWebElementAdapter adapter)
    {
      var stringValue = adapter.GetValue();
      return (T) Convert.ChangeType(stringValue, typeof(T));
    }

    public GetConvertedValue(ITarget target) : base(target) {}

    public GetConvertedValue(IWebElement element) : base(element) {}
  }
}
