using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Matchers;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Questions
{
  /// <summary>
  /// Gets the HTML text content of the target.
  /// </summary>
  public class GetText : TargettedQuestion<string>
  {
    /// <summary>
    /// Gets the report of the current instance, for the given actor.
    /// </summary>
    /// <returns>The human-readable report text.</returns>
    /// <param name="actor">An actor for whom to write the report.</param>
    protected override string GetReport(INamed actor)
    {
      return $"{actor.Name} reads {GetTargetName()}.";
    }

    /// <summary>
    /// Gets a <see cref="IElementDataProvider"/> implementation which interrogates the element adapter and
    /// provides the raw answer data.
    /// </summary>
    /// <returns>The data provider.</returns>
    protected override IElementDataProvider<string> GetDataProvider()
    {
      return new TextMatcher();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetText"/> class from a target.
    /// </summary>
    /// <param name="target">Target.</param>
    public GetText(ITarget target) : base(target) {}

    /// <summary>
    /// Initializes a new instance of the <see cref="GetText"/> class from a Selenium element.
    /// </summary>
    /// <param name="element">Element.</param>
    public GetText(IWebElement element) : base(element) {}
  }
}
