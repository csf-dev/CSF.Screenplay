using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Matchers;
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

    protected override IElementDataProvider<T> GetDataProvider()
    {
      return new ConvertedValueMatcher<T>();
    }

    public GetConvertedValue(ITarget target) : base(target) {}

    public GetConvertedValue(IWebElement element) : base(element) {}
  }
}
