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
  /// Gets the pixel size (height &amp; width) of the target.
  /// </summary>
  public class GetSize : TargettedQuestion<Size>
  {
    /// <summary>
    /// Gets the report of the current instance, for the given actor.
    /// </summary>
    /// <returns>The human-readable report text.</returns>
    /// <param name="actor">An actor for whom to write the report.</param>
    protected override string GetReport(INamed actor)
    {
      return $"{actor.Name} gets the size of {GetTargetName()}.";
    }

    /// <summary>
    /// Gets a <see cref="IElementDataProvider"/> implementation which interrogates the element adapter and
    /// provides the raw answer data.
    /// </summary>
    /// <returns>The data provider.</returns>
    protected override IElementDataProvider<Size> GetDataProvider()
    {
      return new SizeMatcher();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetSize"/> class from a target.
    /// </summary>
    /// <param name="target">Target.</param>
    public GetSize(ITarget target) : base(target) {}

    /// <summary>
    /// Initializes a new instance of the <see cref="GetSize"/> class from a Selenium element.
    /// </summary>
    /// <param name="element">Element.</param>
    public GetSize(IWebElement element) : base(element) {}
  }
}
