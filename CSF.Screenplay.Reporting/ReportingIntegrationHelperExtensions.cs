using System;
using CSF.Screenplay.Reporting;
using CSF.Screenplay.Integration;

namespace CSF.Screenplay
{
  /// <summary>
  /// Extension methods related to integrating an <see cref="IReporter"/> with Screenplay.
  /// </summary>
  public static class ReportingIntegrationHelperExtensions
  {
    public static void UseReporter(this IScreenplayIntegrationHelper helper,
                                   Action<ReporterIntegrationHelper> config)
    {
      if(helper == null)
        throw new ArgumentNullException(nameof(helper));
      if(config == null)
        throw new ArgumentNullException(nameof(config));

      var reportingHelper = new ReporterIntegrationHelper();
      config(reportingHelper);
      reportingHelper.ApplyToIntegration(helper);
    }
  }
}
