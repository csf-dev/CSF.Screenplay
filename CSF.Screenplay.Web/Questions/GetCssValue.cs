using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Questions
{
  public class GetCssValue : TargettedQuestion<string>
  {
    readonly string propertyName;

    protected override string GetReport(INamed actor)
    {
      return $"{actor.Name} reads the CSS property {propertyName} from {GetTargetName()}.";
    }

    protected override string PerformAs(IPerformer actor, BrowseTheWeb ability, IWebElementAdapter adapter)
    {
      return adapter.GetCssValue(propertyName);
    }

    public GetCssValue(ITarget target, string propertyName) : base(target)
    {
      if(propertyName == null)
        throw new ArgumentNullException(nameof(propertyName));

      this.propertyName = propertyName;
    }

    public GetCssValue(IWebElement element, string propertyName) : base(element)
    {
      if(propertyName == null)
        throw new ArgumentNullException(nameof(propertyName));

      this.propertyName = propertyName;
    }
  }
}
