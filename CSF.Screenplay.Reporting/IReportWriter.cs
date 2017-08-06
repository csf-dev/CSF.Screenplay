using System;
using CSF.Screenplay.Reporting.Models;

namespace CSF.Screenplay.Reporting
{
  /// <summary>
  /// A service which can format and write the result of a Screenplay report to some kind of destination.
  /// </summary>
  public interface IReportWriter
  {
    /// <summary>
    /// Write the specified report to the destination.
    /// </summary>
    /// <param name="reportModel">Report model.</param>
    void Write(Report reportModel);
  }
}
