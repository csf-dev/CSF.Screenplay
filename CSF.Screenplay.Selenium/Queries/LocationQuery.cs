﻿using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Web.Models;

namespace CSF.Screenplay.Web.Queries
{
  /// <summary>
  /// Gets the pixel location of the target within the browser window.
  /// </summary>
  public class LocationQuery : Query<Position>
  {
    /// <summary>
    /// Gets the report of the current instance, for the given actor.
    /// </summary>
    /// <returns>The human-readable report text.</returns>
    /// <param name="actor">An actor for whom to write the report.</param>
    /// <param name="targetName">A human-readable name for the target which is reading data.</param>
    protected override string GetQuestionReport(INamed actor, string targetName)
      => $"{actor.Name} gets the screen location of {targetName}.";

    /// <summary>
    /// Gets a description for a match on this value, suitable for an <see cref="ElementMatching.IMatcher"/>
    /// </summary>
    /// <returns>The match description.</returns>
    protected override string GetMatchDescription()
      => "has a matching screen location";

    /// <summary>
    /// Gets the element data.
    /// </summary>
    /// <returns>The element data.</returns>
    /// <param name="adapter">Element.</param>
    protected override Position GetElementData(IWebElementAdapter adapter)
    {
      return adapter.GetLocation();
    }
  }
}
