using System;
namespace CSF.Screenplay.Reporting
{
  /// <summary>
  /// Service which provides a formatted name for an object.
  /// </summary>
  public interface IObjectFormatter
  {
    /// <summary>
    /// Gets a priority for formatting an object.  A formatter which returns a higher priority will be used over one
    /// which returns a lower priority.
    /// </summary>
    /// <remarks>
    /// <para>
    /// It is advised to return a negative priority (-1 is fine) if the formatter cannot format the given
    /// object.  Otherwise return a priority of 1 or higher depending upon how likely the formatter is to
    /// provide a useful result.
    /// </para>
    /// <para>
    /// Priority zero should be considered reserved for the <see cref="DefaultObjectFormatter"/>.
    /// </para>
    /// </remarks>
    /// <returns>The formatting priority.</returns>
    /// <param name="obj">Object.</param>
    int GetFormattingPriority(object obj);

    /// <summary>
    /// Gets a formatted name for the given input.
    /// </summary>
    /// <param name="obj">Object.</param>
    string GetFormattedName(object obj);
  }
}
