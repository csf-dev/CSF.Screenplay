using System;
using System.Collections.Generic;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Matchers;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Questions
{
  /// <summary>
  /// Gets a collection of all of the selected HTML option elements within the target.
  /// </summary>
  public class GetSelectedOptions : TargettedQuestion<IReadOnlyList<Option>>
  {
    /// <summary>
    /// Gets the report of the current instance, for the given actor.
    /// </summary>
    /// <returns>The human-readable report text.</returns>
    /// <param name="actor">An actor for whom to write the report.</param>
    protected override string GetReport(INamed actor)
    {
      return $"{actor.Name} gets the selected options in {GetTargetName()}.";
    }

    /// <summary>
    /// Gets a <see cref="IElementDataProvider"/> implementation which interrogates the element adapter and
    /// provides the raw answer data.
    /// </summary>
    /// <returns>The data provider.</returns>
    protected override IElementDataProvider<IReadOnlyList<Option>> GetDataProvider()
    {
      return new SelectedOptionsMatcher();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetSelectedOptions"/> class from a target.
    /// </summary>
    /// <param name="target">Target.</param>
    public GetSelectedOptions(ITarget target) : base(target) {}

    /// <summary>
    /// Initializes a new instance of the <see cref="GetSelectedOptions"/> class from a Selenium element.
    /// </summary>
    /// <param name="element">Element.</param>
    public GetSelectedOptions(IWebElement element) : base(element) {}
  }
}
