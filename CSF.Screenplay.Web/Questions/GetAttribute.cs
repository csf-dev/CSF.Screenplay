using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Matchers;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Questions
{
  /// <summary>
  /// Gets the value of the named HTML attribute.
  /// </summary>
  public class GetAttribute : TargettedQuestion<string>
  {
    readonly string attributeName;

    /// <summary>
    /// Gets the report of the current instance, for the given actor.
    /// </summary>
    /// <returns>The human-readable report text.</returns>
    /// <param name="actor">An actor for whom to write the report.</param>
    protected override string GetReport(INamed actor)
    {
      return $"{actor.Name} reads the {attributeName} attribute from {GetTargetName()}.";
    }

    /// <summary>
    /// Gets a <see cref="IElementDataProvider"/> implementation which interrogates the element adapter and
    /// provides the raw answer data.
    /// </summary>
    /// <returns>The data provider.</returns>
    protected override IElementDataProvider<string> GetDataProvider()
    {
      return new AttributeMatcher(attributeName);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAttribute"/> class from a target.
    /// </summary>
    /// <param name="target">Target.</param>
    /// <param name="attributeName">Attribute name.</param>
    public GetAttribute(ITarget target, string attributeName) : base(target)
    {
      if(attributeName == null)
        throw new ArgumentNullException(nameof(attributeName));

      this.attributeName = attributeName;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAttribute"/> class from a Selenium element.
    /// </summary>
    /// <param name="element">Element.</param>
    /// <param name="attributeName">Attribute name.</param>
    public GetAttribute(IWebElement element, string attributeName) : base(element)
    {
      if(attributeName == null)
        throw new ArgumentNullException(nameof(attributeName));

      this.attributeName = attributeName;
    }
  }
}
