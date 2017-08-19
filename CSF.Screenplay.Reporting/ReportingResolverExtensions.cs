using System;
using CSF.Screenplay.Reporting;
using CSF.Screenplay.Scenarios;

namespace CSF.Screenplay
{
  public static class ReportingResolverExtensions
  {
    public static IReporter GetReporter(this IServiceResolver resolver, string name = null)
    {
      if(resolver == null)
        throw new ArgumentNullException(nameof(resolver));

      return resolver.GetService<IReporter>(name);
    }

    public static IModelBuildingReporter GetReportBuildingReporter(this IServiceResolver resolver, string name = null)
    {
      if(resolver == null)
        throw new ArgumentNullException(nameof(resolver));

      return resolver.GetService<IModelBuildingReporter>(name);
    }

    public static void SubscribeReporterToActorCreation(this IServiceResolver resolver,
                                                        string reporterName = null,
                                                        string castName = null)
    {
      if(resolver == null)
        throw new ArgumentNullException(nameof(resolver));

      var reporter = resolver.GetReporter(reporterName);
      var cast = resolver.GetCast(castName);

      cast.ActorCreated += (sender, e) => reporter.Subscribe(e.Actor);
    }
  }
}
