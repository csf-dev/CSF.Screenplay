using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Reporting;
using CSF.Screenplay.Reporting.Models;
using CSF.Screenplay.Web.Abilities;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace CSF.Screenplay.NUnit
{
  /// <summary>
  /// Indicates that the assembly contains Screenplay tests.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Assembly,
                  AllowMultiple = false)]
  public class ScreenplayAssemblyAttribute : TestActionAttribute
  {
    /// <summary>
    /// Gets the targets for this attribute (the affected tests).
    /// </summary>
    /// <value>The targets.</value>
    public override ActionTargets Targets => ActionTargets.Suite;

    /// <summary>
    /// Executes actions after any tests in the current assembly.
    /// </summary>
    /// <param name="test">Test.</param>
    public override void AfterTest(ITest test)
    {
      var context = GetContext(test);

      DisposeWebBrowsingAbility(context);
      InformReporterOfCompletion(context);
      WriteReport(context);
    }

    /// <summary>
    /// Executes actions before each affected test.
    /// </summary>
    /// <param name="test">Test.</param>
    public override void BeforeTest(ITest test)
    {
      var context = GetContext(test);

      RegisterCast(context);
      RegisterReporter(context);
      RegisterDefaultWebBrowsingAbility(context);
      ConfigureActorsInCast(context);
    }

    /// <summary>
    /// Registers an implementation of <see cref="ICast"/> with the current context.
    /// </summary>
    protected virtual void RegisterCast(ScreenplayContext context)
    {
      context.RegisterDefaultCast();
    }

    /// <summary>
    /// Registers an implementation of <see cref="Reporting.IReporter"/> with the current context.
    /// </summary>
    protected virtual void RegisterReporter(ScreenplayContext context)
    {
      context.RegisterDefaultReportBuildingReporter();
    }

    /// <summary>
    /// Adds event listeners to the current <see cref="ICast"/> registered in the context.
    /// </summary>
    protected void ConfigureActorsInCast(ScreenplayContext context)
    {
      var cast = context.GetCast();
      if(cast == null)
        return;

      SetupReportingUponCast(cast, context);
    }

    /// <summary>
    /// Sets up the cast such that any newly-created actors are reported-upon.
    /// </summary>
    /// <param name="cast">Cast.</param>
    /// <param name="context">Context.</param>
    protected void SetupReportingUponCast(ICast cast, ScreenplayContext context)
    {
      var reporter = context.GetReporter();
      if(reporter == null)
        return;

      var reportingHelper = GetReportingHelper(cast, reporter, context);
      reportingHelper.SetupSubscriptions();
    }

    /// <summary>
    /// Gets a helper type which sets up reporting for the cast.
    /// </summary>
    /// <returns>The reporting helper.</returns>
    /// <param name="cast">Cast.</param>
    /// <param name="reporter">Reporter.</param>
    protected virtual CastReportingHelper GetReportingHelper(ICast cast, IReporter reporter, ScreenplayContext context)
      => new CastReportingHelper(cast, reporter, context);

    /// <summary>
    /// Informs the reporter (from the current context) that the test run has completed.
    /// </summary>
    protected virtual void InformReporterOfCompletion(ScreenplayContext context)
    {
      var reporter = context.GetReporter();
      if(reporter == null)
        return;

      reporter.CompleteTestRun();
    }

    /// <summary>
    /// Gets the <see cref="Reporting.IReporter"/> from the current context and uses it to write a report.
    /// </summary>
    protected virtual void WriteReport(ScreenplayContext context)
    {
      var report = GetReportModel(context);
      if(report == null)
        return;

      WriteReport(report);
    }

    /// <summary>
    /// Writes a report using the given report model.
    /// </summary>
    /// <param name="report">Report.</param>
    protected virtual void WriteReport(Report report)
    {
      // Intentional no-op, subclasses may override this
    }

    /// <summary>
    /// Gets a report model from the <see cref="Reporting.IReporter"/> which is registered in the current context.
    /// </summary>
    /// <returns>The report model.</returns>
    protected Report GetReportModel(ScreenplayContext context)
    {
      var reporter = context.GetReportBuildingReporter();
      if(reporter == null)
        return null;

      return reporter.GetReport();
    }

    /// <summary>
    /// Gets a URI transformer.
    /// </summary>
    /// <returns>The URI transformer.</returns>
    protected virtual IUriTransformer GetUriTransformer()
    {
      return null;
    }

    /// <summary>
    /// Registers the default web browsing ability with the current context.
    /// </summary>
    protected virtual void RegisterDefaultWebBrowsingAbility(ScreenplayContext context)
    {
      var transformer = GetUriTransformer();
      context.RegisterSingletonBrowseTheWebAbility(transformer);
    }

    /// <summary>
    /// Disposes the web browsing ability from the current context.
    /// </summary>
    protected virtual void DisposeWebBrowsingAbility(ScreenplayContext context)
    {
      var ability = context.GetWebBrowsingAbility();
      if(ability == null)
        return;

      ability.WebDriver.Dispose();
    }

    /// <summary>
    /// Gets the current screenplay context.
    /// </summary>
    /// <value>The context.</value>
    protected ScreenplayContext GetContext(ITest test)
      => ScreenplayContextContainer.GetContext(test.Fixture);
  }
}
