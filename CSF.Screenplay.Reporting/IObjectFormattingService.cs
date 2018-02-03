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

    /// <summary>
    /// Gets a value which indicates whether or not the current formatting service has explicit support
    /// for the given object or not.
    /// </summary>
    /// <remarks>
    /// <para>
    /// If this method returns <c>false</c> then chances are that the output from <see cref="Format"/> will be the
    /// built-in <c>Object.ToString</c>.
    /// </para>
    /// </remarks>
    /// <returns><c>true</c>, if the formatter has explicit support for the given object, <c>false</c> otherwise.</returns>
    /// <param name="obj">Object.</param>
    bool HasExplicitSupport(object obj);
  }
}
