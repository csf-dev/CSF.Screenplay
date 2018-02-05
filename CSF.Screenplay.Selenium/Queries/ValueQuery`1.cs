using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Web.Models;

namespace CSF.Screenplay.Web.Queries
{
  /// <summary>
  /// Gets the HTML <c>value</c> attribute for the element, converted to an alternative data-type (such as a numeric
  /// one).
  /// </summary>
  public class ValueQuery<T> : Query<T>
  {
    /// <summary>
    /// Gets the report of the current instance, for the given actor.
    /// </summary>
    /// <returns>The human-readable report text.</returns>
    /// <param name="actor">An actor for whom to write the report.</param>
    /// <param name="targetName">A human-readable name for the target which is reading data.</param>
    protected override string GetQuestionReport(INamed actor, string targetName)
    => $"{actor.Name} reads the value of {targetName}.";

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
    protected override T GetElementData(IWebElementAdapter adapter)
    {
      var val = adapter.GetValue();
      return (T) Convert.ChangeType(val, typeof(T));
    }
  }
}
