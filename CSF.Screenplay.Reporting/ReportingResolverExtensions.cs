using System;
using CSF.Screenplay.Reporting;
using CSF.Screenplay.Scenarios;

namespace CSF.Screenplay
{
  /// <summary>
  /// Extension methods related to the resolving of reporting-related services.
  /// </summary>
  public static class ReportingResolverExtensions
  {
    /// <summary>
    /// Gets the reporter from the current service resolver.
    /// </summary>
    /// <returns>The reporter.</returns>
    /// <param name="resolver">Resolver.</param>
    /// <param name="name">Name.</param>
    public static IReporter GetReporter(this IServiceResolver resolver, string name = null)
    {
      if(resolver == null)
        throw new ArgumentNullException(nameof(resolver));

      return resolver.GetService<IReporter>(name);
    }

    /// <summary>
    /// Gets the reporter from the current service resolver, expecting that reporter to be an instance of
    /// <see cref="IModelBuildingReporter"/>.
    /// </summary>
    /// <returns>The reporter.</returns>
    /// <param name="resolver">Resolver.</param>
    /// <param name="name">Name.</param>
    public static IModelBuildingReporter GetReportBuildingReporter(this IServiceResolver resolver, string name = null)
    {
      if(resolver == null)
        throw new ArgumentNullException(nameof(resolver));

      return resolver.GetService<IModelBuildingReporter>(name);
    }

    /// <summary>
    /// Configures the reporter from the current resolver to subscribe to the current <see cref="Actors.ICast"/>,
    /// subscribing to any actors which are newly-created within that cast.
    /// </summary>
    /// <param name="resolver">Resolver.</param>
    /// <param name="reporterName">Reporter name.</param>
    /// <param name="castName">Cast name.</param>
    public static void SubscribeReporterToCastActorCreation(this IServiceResolver resolver,
                                                            string reporterName = null,
                                                            string castName = null)
    {
      if(resolver == null)
        throw new ArgumentNullException(nameof(resolver));

      var reporter = resolver.GetReporter(reporterName);
      var cast = resolver.GetCast(castName);

      cast.ActorCreated += (sender, e) => reporter.Subscribe(e.Actor);
    }

    /// <summary>
    /// Subscribes the reporter to scenario-related events.
    /// </summary>
    /// <param name="scenario">Scenario.</param>
    /// <param name="reporterName">Reporter name.</param>
    public static void SubscribeReporterToScenarioEvents(this IScreenplayScenario scenario,
                                                         string reporterName = null)
    {
      if(scenario == null)
        throw new ArgumentNullException(nameof(scenario));

      var reporter = scenario.GetReporter(reporterName);
      reporter.Subscribe(scenario);
    }
  }
}
