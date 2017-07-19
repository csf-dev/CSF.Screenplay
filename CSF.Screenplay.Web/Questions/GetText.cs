using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Matchers;
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

    protected override IElementDataProvider<string> GetDataProvider()
    {
      return new TextMatcher();
    }

    public GetText(ITarget target) : base(target) {}

    public GetText(IWebElement element) : base(element) {}
  }
}
