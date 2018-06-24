using System;
using CSF.Screenplay.ReportModel;

namespace CSF.Screenplay.Reporting
{
  /// <summary>
  /// An object which can provide an <see cref="IReport"/> report model.
  /// </summary>
  public interface IGetsReportModel
  {
    /// <summary>
    /// Gets the report model.
    /// </summary>
    /// <returns>The report.</returns>
    IReport GetReport();
  }
}
