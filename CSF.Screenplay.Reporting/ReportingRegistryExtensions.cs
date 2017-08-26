using System;
using CSF.Screenplay.Reporting;
using CSF.Screenplay.Scenarios;

namespace CSF.Screenplay
{
  /// <summary>
  /// Extension methods related to registering reporting-related components with the current registry builder.
  /// </summary>
  public static class ReportingRegistryExtensions
  {
    /// <summary>
    /// Registers a single reporter instance with the registry builder.
    /// </summary>
    /// <param name="builder">Builder.</param>
    /// <param name="reporter">Reporter.</param>
    /// <param name="name">Name.</param>
    public static void RegisterReporter(this IServiceRegistryBuilder builder,
                                        IReporter reporter = null,
                                        string name = null)
    {
      if(builder == null)
        throw new ArgumentNullException(nameof(builder));

      reporter = reporter?? new ReportBuildingReporter();

      builder.RegisterSingleton(reporter, name);
      if(reporter is IModelBuildingReporter)
      {
        builder.RegisterSingleton((IModelBuildingReporter) reporter, name);
      }
    }
  }
}
