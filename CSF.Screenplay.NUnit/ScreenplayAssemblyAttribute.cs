using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Context;
using CSF.Screenplay.Reporting;
using CSF.Screenplay.Reporting.Models;
using CSF.Screenplay.Web.Abilities;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace CSF.Screenplay.NUnit
{
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
      DisposeWebBrowsingAbility();
      InformReporterOfCompletion();
      WriteReport();
    }

    /// <summary>
    /// Executes actions before each affected test.
    /// </summary>
    /// <param name="test">Test.</param>
    public override void BeforeTest(ITest test)
    {
      RegisterCast();
      RegisterReporter();
      RegisterDefaultWebBrowsingAbility();
      ConfigureActorsInCast();
    }

    protected virtual void RegisterCast()
    {
      Context.RegisterDefaultCast();
    }

    protected virtual void RegisterReporter()
    {
      Context.RegisterDefaultReportBuildingReporter();
    }

    protected void ConfigureActorsInCast()
    {
      var cast = Context.GetCast();
      if(cast == null)
        return;

      ConfigureActorsInCast(cast);
    }

    protected virtual void ConfigureActorsInCast(ICast cast)
    {
      cast.ActorAdded += HandleActorAddedToCast;
      cast.ActorCreated += HandleActorCreatedInCast;
    }

    protected virtual void HandleActorCreatedInCast(object sender, ActorEventArgs args)
    {
      SubscribeReporter(args.Actor);
    }

    protected virtual void HandleActorAddedToCast(object sender, ActorEventArgs args)
    {
      // Intentional no-op, method is here for subclasses to override.
    }

    protected virtual void SubscribeReporter(IActor actor)
    {
      var reporter = Context.GetReporter();
      if(reporter == null)
        return;

      reporter.Subscribe(actor);
    }

    protected virtual void InformReporterOfCompletion()
    {
      var reporter = Context.GetReporter();
      if(reporter == null)
        return;

      reporter.CompleteTestRun();
    }

    protected virtual void WriteReport()
    {
      var report = GetReportModel();
      if(report == null)
        return;

      WriteReport(report);
    }

    protected virtual void WriteReport(Report report)
    {
      // Intentional no-op, subclasses may override this
    }

    protected Report GetReportModel()
    {
      var reporter = Context.GetReportBuildingReporter();
      if(reporter == null)
        return null;

      return reporter.GetReport();
    }

    protected virtual IUriTransformer GetUriTransformer()
    {
      return null;
    }

    protected virtual void RegisterDefaultWebBrowsingAbility()
    {
      var transformer = GetUriTransformer();
      Context.RegisterSingletonBrowseTheWebAbility(transformer);
    }

    protected virtual void DisposeWebBrowsingAbility()
    {
      var ability = Context.GetWebBrowsingAbility();
      if(ability == null)
        return;

      ability.WebDriver.Dispose();
    }

    /// <summary>
    /// Gets the current screenplay context.
    /// </summary>
    /// <value>The context.</value>
    protected ScreenplayContext Context => ScreenplayContext.Current;
  }
}
