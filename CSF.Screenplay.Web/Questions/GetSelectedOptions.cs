using System;
using System.Collections.Generic;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Matchers;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Questions
{
  public class GetSelectedOptions : TargettedQuestion<IReadOnlyList<Option>>
  {
    protected override string GetReport(INamed actor)
    {
      return $"{actor.Name} gets the selected options in {GetTargetName()}.";
    }

    protected override IElementDataProvider<IReadOnlyList<Option>> GetDataProvider()
    {
      return new SelectedOptionsMatcher();
    }

    public GetSelectedOptions(ITarget target) : base(target) {}

    public GetSelectedOptions(IWebElement element) : base(element) {}
  }
}
