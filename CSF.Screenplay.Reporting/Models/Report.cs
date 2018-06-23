using System;
using System.Collections.Generic;
using System.Linq;

namespace CSF.Screenplay.Reporting.Models
{
  /// <summary>
  /// The root of the report model, representing the report for an entire test run.
  /// </summary>
  public class Report : IReport
  {
    ICollection<Scenario> scenarios;

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
    /// Gets the timestamp for the creation of this report.
    /// </summary>
    /// <value>The timestamp.</value>
    public DateTime Timestamp { get; set; }

    IEnumerable<IScenario> IProvidesScenarios.Scenarios => Scenarios.Cast<IScenario>();

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Reporting.Models.Report"/> class.
    /// </summary>
    public Report()
    {
      scenarios = new List<Scenario>();
    }
  }
}
