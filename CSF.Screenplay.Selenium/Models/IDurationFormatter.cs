using System;
namespace CSF.Screenplay.Web.Models
{
  /// <summary>
  /// Formats a <c>System.TimeSpan</c> as a human-readable string.
  /// </summary>
  public interface IDurationFormatter
  {
    /// <summary>
    /// Gets the human-readable representation of the given timespan.
    /// </summary>
    /// <returns>The duration.</returns>
    /// <param name="timespan">Timespan.</param>
    string GetDuration(TimeSpan timespan);
  }
}
