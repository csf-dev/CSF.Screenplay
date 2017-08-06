using System;
using CSF.Screenplay.Context;

namespace CSF.Screenplay
{
  /// <summary>
  /// Extension methods related to registering and retrieving the <see cref="IReporter"/> associated with the current
  /// context.
  /// </summary>
  public static class ScreenplayContextReportingExtensions
  {
    /// <summary>
    /// Registers the reporter with the context.  This reporter instance is used for all test cases/scenarios
    /// in the test run.
    /// </summary>
    /// <param name="context">Context.</param>
    /// <param name="reporter">Reporter.</param>
    /// <param name="name">An optional identifying name for the reporter instance.</param>
    public static void RegisterReporter(this IScreenplayContext context, IReporter reporter, string name = null)
    {
      if(context == null)
        throw new ArgumentNullException(nameof(context));
      if(reporter == null)
        throw new ArgumentNullException(nameof(reporter));

      context.RegisterSingleton(reporter, name);
    }

    /// <summary>
    /// Adds a registration to the screenplay context for a default <see cref="ReportBuildingReporter"/>
    /// instance, registered for all test cases/scenarios in the test run.
    /// </summary>
    /// <returns>The created reporter instance.</returns>
    /// <param name="context">Context.</param>
    public static ReportBuildingReporter RegisterDefaultReportBuildingReporter(this IScreenplayContext context)
    {
      if(context == null)
        throw new ArgumentNullException(nameof(context));

      var reporter = new ReportBuildingReporter();
      RegisterReporter(context, reporter);
      return reporter;
    }

    /// <summary>
    /// Gets the previously-registered reporter.
    /// </summary>
    /// <returns>The reporter.</returns>
    /// <param name="context">Context.</param>
    /// <param name="name">An optional identifying name for the reporter instance.</param>
    public static IReporter GetReporter(this IScreenplayContext context, string name = null)
    {
      if(context == null)
        throw new ArgumentNullException(nameof(context));

      return context.GetService<IReporter>(name);
    }

    /// <summary>
    /// Gets the previously-registered reporter, returning it as a <see cref="IModelBuildingReporter"/>.
    /// </summary>
    /// <returns>The reporter.</returns>
    /// <param name="context">Context.</param>
    /// <param name="name">An optional identifying name for the reporter instance.</param>
    public static IModelBuildingReporter GetReportBuildingReporter(this IScreenplayContext context, string name = null)
    {
      if(context == null)
        throw new ArgumentNullException(nameof(context));

      return context.GetService<IReporter>(name) as IModelBuildingReporter;
    }
  }
}
