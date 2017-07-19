using System;
using System.Collections.Generic;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Matchers;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Questions
{
  public class GetAllOptions : TargettedQuestion<IReadOnlyList<Option>>
  {
    protected override string GetReport(INamed actor)
    {
      return $"{actor.Name} gets the options from {GetTargetName()}.";
    }

    protected override IElementDataProvider<IReadOnlyList<Option>> GetDataProvider()
    {
      return new OptionsMatcher();
    }

    public GetAllOptions(ITarget target) : base(target) {}

    public GetAllOptions(IWebElement element) : base(element) {}
  }
}
