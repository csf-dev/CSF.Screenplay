using System;
using CSF.Screenplay.Reporting;
using CSF.Screenplay.Integration;

namespace CSF.Screenplay
{
  /// <summary>
  /// Extension methods related to integrating an <see cref="IReporter"/> with Screenplay.
  /// </summary>
  public static class ReportingIntegrationBuilderExtensions
  {
    /// <summary>
    /// Registers and configures reporting within the Screenplay test run.  A configuration builder is used to
    /// actually configure reporting, as there are several options available.
    /// </summary>
    /// <param name="helper">Helper.</param>
    /// <param name="config">Config.</param>
    public static void UseReporting(this IIntegrationConfigBuilder helper,
                                    Action<ReportingIntegrationBuilder> config)
    {
      if(helper == null)
        throw new ArgumentNullException(nameof(helper));
      if(config == null)
        throw new ArgumentNullException(nameof(config));

      var reportingHelper = new ReportingIntegrationBuilder();
      config(reportingHelper);
      reportingHelper.ApplyToIntegration(helper);
    }
  }
}
