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
    /// Gets the object formatting service from the current resolver.
    /// </summary>
    /// <returns>The object formatting service.</returns>
    /// <param name="resolver">Resolver.</param>
    /// <param name="name">Name.</param>
    public static IObjectFormattingService GetObjectFormattingService(this IServiceResolver resolver, string name = null)
    {
      if(resolver == null)
        throw new ArgumentNullException(nameof(resolver));

      return resolver.GetService<IObjectFormattingService>(name);
    }
  }
}
