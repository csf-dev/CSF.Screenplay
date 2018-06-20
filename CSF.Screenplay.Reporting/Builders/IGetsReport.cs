using System.Collections.Generic;
using CSF.Screenplay.Reporting.Models;

namespace CSF.Screenplay.Reporting.Builders
{
  /// <summary>
  /// Factory service which creates instances of <see cref="Report"/>.
  /// </summary>
  public interface IGetsReport
  {
    Report GetReport(IEnumerable<IBuildsScenario> scenarioBuilders);

    /// <summary>
    /// Creates and returns a <see cref="Report"/> instance.
    /// </summary>
    /// <param name="scenarios"></param>
    /// <returns></returns>
    Report GetReport(IEnumerable<Scenario> scenarios);
  }
}