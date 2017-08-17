using System;
using TechTalk.SpecFlow;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Reporting;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Reporting.Models;

namespace CSF.Screenplay.SpecFlow
{
  public class ScreenplayHooks
  {
    #region fields

    static readonly ScreenplayContext context;
    readonly BeforeAfterTestHelper beforeAfterHelper;

    #endregion

    #region properties

    public ScreenplayContext Context => context;

    #endregion

    #region before test run

    /// <summary>
    /// Registers an implementation of <see cref="ICast"/> with the current context.
    /// </summary>
    protected static void RegisterCast()
    {
      context.RegisterDefaultCast();
    }

    /// <summary>
    /// Registers an implementation of <see cref="IReporter"/> with the current context.
    /// </summary>
    protected static void RegisterReporter()
    {
      context.RegisterDefaultReportBuildingReporter();
    }

    /// <summary>
    /// Adds event listeners to the current <see cref="ICast"/> registered in the context.
    /// </summary>
    protected static void ConfigureActorsInCast()
    {
      var cast = context.GetCast();
      if(cast == null)
        return;

      SetupReportingUponCast(cast);
    }

    /// <summary>
    /// Sets up the cast such that any newly-created actors are reported-upon.
    /// </summary>
    /// <param name="cast">Cast.</param>
    protected static void SetupReportingUponCast(ICast cast)
    {
      var reporter = context.GetReporter();
      if(reporter == null)
        return;

      var reportingHelper = GetReportingHelper(cast, reporter);
      reportingHelper.SetupSubscriptions();
    }

    /// <summary>
    /// Gets a helper type which sets up reporting for the cast.
    /// </summary>
    /// <returns>The reporting helper.</returns>
    /// <param name="cast">Cast.</param>
    /// <param name="reporter">Reporter.</param>
    protected static CastReportingHelper GetReportingHelper(ICast cast, IReporter reporter)
      => new CastReportingHelper(cast, reporter, context);

    /// <summary>
    /// Gets a URI transformer.
    /// </summary>
    /// <returns>The URI transformer.</returns>
    protected static IUriTransformer GetUriTransformer()
    {
      return null;
    }

    /// <summary>
    /// Registers the default web browsing ability with the current context.
    /// </summary>
    protected static void RegisterDefaultWebBrowsingAbility()
    {
      var transformer = GetUriTransformer();
      context.RegisterSingletonBrowseTheWebAbility(transformer);
    }

    #endregion

    #region after test run (helpers)

    /// <summary>
    /// Disposes the web browsing ability from the current context.
    /// </summary>
    protected static void DisposeWebBrowsingAbility()
    {
      var ability = context.GetWebBrowsingAbility();
      if(ability == null)
        return;

      ability.WebDriver.Dispose();
    }

    /// <summary>
    /// Informs the reporter (from the current context) that the test run has completed.
    /// </summary>
    protected static void InformReporterOfCompletion()
    {
      var reporter = context.GetReporter();
      if(reporter == null)
        return;

      reporter.CompleteTestRun();
    }

    /// <summary>
    /// Gets a report model from the <see cref="IReporter"/> which is registered in the current context.
    /// </summary>
    /// <returns>The report model.</returns>
    protected static Report GetReportModel()
    {
      var reporter = context.GetReportBuildingReporter();
      if(reporter == null)
        return null;

      return reporter.GetReport();
    }

    #endregion

    #region before scenario

    [Before]
    public virtual void BeforeScenario()
    {
      beforeAfterHelper.BeforeScenario(context, ScenarioContext.Current, FeatureContext.Current);
    }

    #endregion

    #region after scenario

    [After]
    public virtual void AfterScenario()
    {
      beforeAfterHelper.AfterScenario(context, ScenarioContext.Current, FeatureContext.Current);
    }

    #endregion

    #region constructors

    public ScreenplayHooks()
    {
      beforeAfterHelper = new BeforeAfterTestHelper();
    }

    static ScreenplayHooks()
    {
      context = new ScreenplayContext();
    }

    #endregion
  }
}
