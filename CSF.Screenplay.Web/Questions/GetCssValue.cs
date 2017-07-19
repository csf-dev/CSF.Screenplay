using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Matchers;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Questions
{
  public class GetCssValue : TargettedQuestion<string>
  {
    readonly string propertyName;

    /// <summary>
    /// Gets the report of the current instance, for the given actor.
    /// </summary>
    /// <returns>The human-readable report text.</returns>
    /// <param name="actor">An actor for whom to write the report.</param>
    protected override string GetReport(INamed actor)
    {
      return $"{actor.Name} reads the CSS property {propertyName} from {GetTargetName()}.";
    }

    /// <summary>
    /// Gets a <see cref="IElementDataProvider"/> implementation which interrogates the element adapter and
    /// provides the raw answer data.
    /// </summary>
    /// <returns>The data provider.</returns>
    protected override IElementDataProvider<string> GetDataProvider()
    {
      return new CssValueMatcher(propertyName);
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
