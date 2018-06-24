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
    readonly IGetsReportMetadata metadataFactory;

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
        Metadata = metadataFactory.GetReportMetadata(),
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

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Reporting.Builders.ReportFactory"/> class.
    /// </summary>
    public ReportFactory() : this(new ReportMetadataFactory()) {}

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Reporting.Builders.ReportFactory"/> class.
    /// </summary>
    /// <param name="metadataFactory">Metadata factory.</param>
    public ReportFactory(IGetsReportMetadata metadataFactory)
    {
      if(metadataFactory == null)
        throw new ArgumentNullException(nameof(metadataFactory));
      this.metadataFactory = metadataFactory;
    }
  }
}