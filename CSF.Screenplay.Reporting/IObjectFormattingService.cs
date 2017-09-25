using System;
namespace CSF.Screenplay.Reporting
{
  /// <summary>
  /// Formats an object into a human-readable string.
  /// </summary>
  public interface IObjectFormattingService
  {
    /// <summary>
    /// Format the specified object.
    /// </summary>
    /// <param name="obj">Object.</param>
    string Format(object obj);
  }
}
