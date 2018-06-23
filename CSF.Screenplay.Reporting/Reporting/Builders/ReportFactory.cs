using System;
using System.Collections.Generic;
using System.Linq;
using CSF.Screenplay.ReportModel;

namespace CSF.Screenplay.Reporting.Builders
{
  /// <summary>
  /// Factory service which creates instances of <see cref="Report"/>.
  /// </summary>
  public class ReportFactory : IGetsReport
  {
    /// <summary>
    /// Creates and returns a <see cref="Report"/> instance.
    /// </summary>
    /// <param name="scenarios"></param>
    /// <returns></returns>
    public IReport GetReport(IEnumerable<Scenario> scenarios)
    {
      if(scenarios == null)
        throw new ArgumentNullException(nameof(scenarios));

      return new Report {
        Timestamp = DateTime.UtcNow,
        Scenarios = scenarios.ToList(),
      };
    }

    /// <summary>
    /// Creates and returns a <see cref="T:CSF.Screenplay.Reporting.Models.Report" /> instance.
    /// </summary>
    /// <returns>The report.</returns>
    /// <param name="scenarioBuilders">Scenario builders.</param>
    public IReport GetReport(IEnumerable<IBuildsScenario> scenarioBuilders)
    {
      if(scenarioBuilders == null)
        throw new ArgumentNullException(nameof(scenarioBuilders));
      
      var scenarios = scenarioBuilders
        .Select(x => x.GetScenario())
        .Where(x => x != null)
        .ToArray();
      
      return GetReport(scenarios);
    }
  }
}