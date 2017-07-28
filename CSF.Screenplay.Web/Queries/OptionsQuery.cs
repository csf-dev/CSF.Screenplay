using System;
using System.Collections.Generic;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Web.Models;

namespace CSF.Screenplay.Web.Queries
{
  /// <summary>
  /// Gets a collection of all of the HTML option elements within the target.
  /// </summary>
  public class OptionsQuery : Query<IReadOnlyList<Option>>
  {
    /// <summary>
    /// Gets the report of the current instance, for the given actor.
    /// </summary>
    /// <returns>The human-readable report text.</returns>
    /// <param name="actor">An actor for whom to write the report.</param>
    /// <param name="targetName">A human-readable name for the target which is reading data.</param>
    protected override string GetQuestionReport(INamed actor, string targetName)
      => $"{actor.Name} gets the options from {targetName}.";

    /// <summary>
    /// Gets a description for a match on this value, suitable for an <see cref="ElementMatching.IMatcher"/>
    /// </summary>
    /// <returns>The match description.</returns>
    protected override string GetMatchDescription()
      => "has matching options";

    /// <summary>
    /// Gets the element data.
    /// </summary>
    /// <returns>The element data.</returns>
    /// <param name="adapter">Element.</param>
    protected override IReadOnlyList<Option> GetElementData(IWebElementAdapter adapter)
    {
      return adapter.GetAllOptions();
    }
  }
}
