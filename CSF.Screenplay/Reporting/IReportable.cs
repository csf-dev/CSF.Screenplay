using System;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay.Reporting
{
  /// <summary>
  /// Indicates a type which can provide a human-readable report text indicating its nature.
  /// </summary>
  public interface IReportable
  {
    /// <summary>
    /// Gets the report of the current instance, for the given actor.
    /// </summary>
    /// <returns>The human-readable report text.</returns>
    /// <param name="actor">An actor for whom to write the report.</param>
    string GetReport(INamed actor);
  }
}
