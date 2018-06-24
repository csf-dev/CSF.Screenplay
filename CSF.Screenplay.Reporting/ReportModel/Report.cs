using System;
using System.Collections.Generic;
using System.Linq;

namespace CSF.Screenplay.ReportModel
{
  /// <summary>
  /// The root of the report model, representing the report for an entire test run.
  /// </summary>
  public class Report : IReport
  {
    ICollection<Scenario> scenarios;
    ReportMetadata metadata;

    /// <summary>
    /// Gets the scenarios in the current report instance.
    /// </summary>
    /// <value>The scenarios.</value>
    public ICollection<Scenario> Scenarios
    {
      get { return scenarios; }
      set { scenarios = value ?? new List<Scenario>(); }
    }

    /// <summary>
    /// Gets or sets the metadata.
    /// </summary>
    /// <value>The metadata.</value>
    public ReportMetadata Metadata
    {
      get { return metadata; }
      set { metadata = value ?? new ReportMetadata(); }
    }

    IEnumerable<IScenario> IProvidesScenarios.Scenarios => Scenarios.Cast<IScenario>();

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Reporting.Models.Report"/> class.
    /// </summary>
    public Report()
    {
      scenarios = new List<Scenario>();
      metadata = new ReportMetadata();
    }
  }
}
