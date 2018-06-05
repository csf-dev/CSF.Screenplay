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
    /// Gets or sets a value indicating whether this reporter should mark scenario identifiers as auto-generated and
    /// thus meaningless in reports.
    /// </summary>
    /// <value><c>true</c> if the reporter should mark scenario identifiers as generated; otherwise, <c>false</c>.</value>
    bool MarkScenarioIdsAsGenerated { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this reporter should mark feature identifiers as auto-generated and
    /// thus meaningless in reports.
    /// </summary>
    /// <value><c>true</c> if the reporter should mark feature identifiers as generated; otherwise, <c>false</c>.</value>
    bool MarkFeatureIdsAsGenerated { get; set; }

    /// <summary>
    /// Gets the report model.
    /// </summary>
    /// <returns>The report.</returns>
    Report GetReport();
  }
}
