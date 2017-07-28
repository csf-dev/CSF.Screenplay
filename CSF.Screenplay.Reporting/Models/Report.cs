using System;
using System.Collections.Generic;

namespace CSF.Screenplay.Reporting.Models
{
  /// <summary>
  /// The root of the report model, representing the report for an entire test run.
  /// </summary>
  public class Report
  {
    readonly IReadOnlyList<Scenario> scenarios;
    readonly DateTime timestamp;

    /// <summary>
    /// Gets a collection of the scenarios in this report.
    /// </summary>
    /// <value>The scenarios.</value>
    public virtual IReadOnlyList<Scenario> Scenarios => scenarios;

    /// <summary>
    /// Gets the timestamp for the creation of this report.
    /// </summary>
    /// <value>The timestamp.</value>
    public virtual DateTime Timestamp => timestamp;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Reporting.Models.Report"/> class.
    /// </summary>
    /// <param name="scenarios">Scenarios.</param>
    public Report(IReadOnlyList<Scenario> scenarios)
    {
      if(scenarios == null)
        throw new ArgumentNullException(nameof(scenarios));
      
      this.scenarios = scenarios;
      timestamp = DateTime.Now;
    }
  }
}
