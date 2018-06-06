using System;
using CSF.Screenplay.Reporting.Models;

namespace CSF.Screenplay.Reporting
{
  /// <summary>
  /// Implementation of an <see cref="IReporter"/> which creates a report model.
  /// </summary>
  public interface IModelBuildingReporter : IReporter
  {
    /// <summary>
    /// Gets the report model.
    /// </summary>
    /// <returns>The report.</returns>
    Report GetReport();
  }
}
