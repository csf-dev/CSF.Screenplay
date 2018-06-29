using System;
namespace CSF.Screenplay.ReportFormatting
{
  /// <summary>
  /// An object which gets a formatted <c>System.String</c> for a given object, for the purpose of writing a Screenplay
  /// report.
  /// </summary>
  public interface IFormatsObjectForReport
  {
    /// <summary>
    /// Gets a formatted string representing the given object.
    /// </summary>
    /// <param name="obj">The object to format.</param>
    string FormatForReport(object obj);
  }
}
