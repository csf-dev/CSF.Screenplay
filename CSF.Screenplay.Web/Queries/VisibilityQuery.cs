using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Web.Models;

namespace CSF.Screenplay.Web.Queries
{
  /// <summary>
  /// Query which determines whether a given element is visible or not.
  /// </summary>
  public class VisibilityQuery : Query<bool>
  {
    /// <summary>
    /// Gets the element data.
    /// </summary>
    /// <returns>The element data.</returns>
    /// <param name="adapter">Adapter.</param>
    protected override bool GetElementData(IWebElementAdapter adapter)
    {
      return adapter != null && adapter.IsVisible();
    }

    /// <summary>
    /// Gets a description for a match on this value, suitable for an <see cref="ElementMatching.IMatcher" />
    /// </summary>
    /// <returns>The match description.</returns>
    protected override string GetMatchDescription() => "has matching visibility";

    /// <summary>
    /// Gets a report appropriate to a question which gets this value from a target.
    /// </summary>
    /// <returns>The question report.</returns>
    /// <param name="actor">Actor.</param>
    /// <param name="targetName">Target name.</param>
    protected override string GetQuestionReport(INamed actor, string targetName)
      => $"{actor.Name} sees whether {targetName} is visible or not.";
  }
}
