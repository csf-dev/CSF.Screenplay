using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Selenium.Models;

namespace CSF.Screenplay.Selenium.Queries
{
  /// <summary>
  /// Gets the HTML <c>value</c> attribute for the element.
  /// </summary>
  public class ValueQuery : Query<string>
  {
    /// <summary>
    /// Gets the report of the current instance, for the given actor.
    /// </summary>
    /// <returns>The human-readable report text.</returns>
    /// <param name="actor">An actor for whom to write the report.</param>
    /// <param name="targetName">A human-readable name for the target which is reading data.</param>
    protected override string GetQuestionReport(INamed actor, string targetName)
    => $"{actor.Name} reads the value from {targetName}.";

    /// <summary>
    /// Gets a description for a match on this value, suitable for an <see cref="ElementMatching.IMatcher"/>
    /// </summary>
    /// <returns>The match description.</returns>
    protected override string GetMatchDescription()
    => "has a matching value";

    /// <summary>
    /// Gets the element data.
    /// </summary>
    /// <returns>The element data.</returns>
    /// <param name="adapter">Element.</param>
    protected override string GetElementData(IWebElementAdapter adapter)
    {
      return adapter.GetValue();
    }
  }
}
