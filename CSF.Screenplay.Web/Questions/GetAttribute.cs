using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Matchers;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Questions
{
  public class GetAttribute : TargettedQuestion<string>
  {
    readonly string attributeName;

    protected override string GetReport(INamed actor)
    {
      return $"{actor.Name} reads the {attributeName} attribute from {GetTargetName()}.";
    }

    protected override IElementDataProvider<string> GetDataProvider()
    {
      return new AttributeMatcher(attributeName);
    }

    public GetAttribute(ITarget target, string attributeName) : base(target)
    {
      if(attributeName == null)
        throw new ArgumentNullException(nameof(attributeName));

      this.attributeName = attributeName;
    }

    public GetAttribute(IWebElement element, string attributeName) : base(element)
    {
      if(attributeName == null)
        throw new ArgumentNullException(nameof(attributeName));

      this.attributeName = attributeName;
    }
  }
}
