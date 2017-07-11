using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Questions
{
  public class GetConvertedText<T> : TargettedQuestion<T>
  {
    protected override string GetReport(INamed actor)
    {
      return $"{actor.Name} reads {GetTargetName()}.";
    }

    protected override T PerformAs(IPerformer actor, BrowseTheWeb ability, IWebElementAdapter adapter)
    {
      var stringValue = adapter.GetText();
      return (T) Convert.ChangeType(stringValue, typeof(T));
    }

    public GetConvertedText(ITarget target) : base(target) {}

    public GetConvertedText(IWebElement element) : base(element) {}
  }
}
