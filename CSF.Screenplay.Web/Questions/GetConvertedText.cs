using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Matchers;
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

    protected override IElementDataProvider<T> GetDataProvider()
    {
      return new ConvertedTextMatcher<T>();
    }

    public GetConvertedText(ITarget target) : base(target) {}

    public GetConvertedText(IWebElement element) : base(element) {}
  }
}
