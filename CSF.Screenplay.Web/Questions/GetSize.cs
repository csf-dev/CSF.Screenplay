using System;
using System.Collections.Generic;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Matchers;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Questions
{
  public class GetSize : TargettedQuestion<Size>
  {
    protected override string GetReport(INamed actor)
    {
      return $"{actor.Name} gets the size of {GetTargetName()}.";
    }

    protected override IElementDataProvider<Size> GetDataProvider()
    {
      return new SizeMatcher();
    }

    public GetSize(ITarget target) : base(target) {}

    public GetSize(IWebElement element) : base(element) {}
  }
}
