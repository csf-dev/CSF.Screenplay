using System;
using System.Collections.Generic;
using System.Linq;
using CSF.Screenplay.Reporting.Builders;

namespace CSF.Screenplay.Reporting.Models
{
  /// <summary>
  /// Factory service which creates instances of <see cref="Report"/>.
  /// </summary>
  public class ReportFactory : IReportFactory
  {
    /// <summary>
    /// Creates and returns a <see cref="Report"/> instance.
    /// </summary>
    /// <param name="scenarios"></param>
    /// <returns></returns>
    public Report GetReport(IReadOnlyCollection<Scenario> scenarios)
    {
      if(scenarios == null)
        throw new ArgumentNullException(nameof(scenarios));

      return new Report {
        Timestamp = DateTime.UtcNow,
        Scenarios = scenarios.ToList(),
      };
    }
  }
}