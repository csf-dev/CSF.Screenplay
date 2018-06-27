using System;

namespace CSF.Screenplay.Stopwatch
{
  /// <summary>
  /// Formats a <c>System.TimeSpan</c> as a human-readable string.
  /// </summary>
  public interface IFormatsDurations
  {
    /// <summary>
    /// Gets the human-readable representation of the given timespan.
    /// </summary>
    /// <returns>The duration.</returns>
    /// <param name="timespan">Timespan.</param>
    string GetDuration(TimeSpan timespan);
  }
}
