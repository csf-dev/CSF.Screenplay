using System;
namespace CSF.Screenplay.Reporting
{
  /// <summary>
  /// A default object formatter which uses the object's <c>.ToString()</c> implementation.
  /// </summary>
  public class DefaultObjectFormatter : IObjectFormatter
  {
    /// <summary>
    /// Gets a priority for formatting an object.  This implementation will always return zero.
    /// </summary>
    /// <returns>The formatting priority.</returns>
    /// <param name="obj">Object.</param>
    public int GetFormattingPriority(object obj) => 0;

    /// <summary>
    /// Gets a formatted name for the given input.
    /// </summary>
    /// <param name="obj">Object.</param>
    public string GetFormattedName(object obj) => obj != null? obj.ToString() : "<null>";
  }
}
