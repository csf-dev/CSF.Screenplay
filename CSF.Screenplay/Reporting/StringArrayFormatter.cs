using System;
namespace CSF.Screenplay.Reporting
{
  /// <summary>
  /// Formatter for string arrays.
  /// </summary>
  public class StringArrayFormatter : ObjectFormatter<string[]>
  {
    /// <summary>
    /// Gets a formatted name for the given input.
    /// </summary>
    /// <param name="obj">Object.</param>
    public override string Format(string[] obj) => String.Join(", ", obj);
  }
}
