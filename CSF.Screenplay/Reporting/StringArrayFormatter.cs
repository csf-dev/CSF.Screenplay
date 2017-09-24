using System;
namespace CSF.Screenplay.Reporting
{
  /// <summary>
  /// Formatter for string arrays.
  /// </summary>
  public class StringArrayFormatter : GenericObjectFormatter<string[]>
  {
    /// <summary>
    /// Gets a formatted name for the given input.
    /// </summary>
    /// <param name="obj">Object.</param>
    protected override string GetFormattedName(string[] obj) => String.Join(", ", obj);
  }
}
