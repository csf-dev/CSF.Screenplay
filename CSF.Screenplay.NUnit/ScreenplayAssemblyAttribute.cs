using System;
using CSF.Screenplay.Actors;
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

    /// <summary>
    /// Registers an implementation of <see cref="ICast"/> with the current context.
    /// </summary>
    protected virtual void RegisterCast()
    {
      Context.RegisterDefaultCast();
    }

    /// <summary>
    /// Registers an implementation of <see cref="Reporting.IReporter"/> with the current context.
    /// </summary>
    protected virtual void RegisterReporter()
    {
      Context.RegisterDefaultReportBuildingReporter();
    }

    /// <summary>
    /// Adds event listeners to the current <see cref="ICast"/> registered in the context.
    /// </summary>
    protected void ConfigureActorsInCast()
    {
      var cast = Context.GetCast();
      if(cast == null)
        return;

      ConfigureActorsInCast(cast);
    }

    /// <summary>
    /// Adds event listeners to the given <see cref="ICast"/> instance.
    /// </summary>
    /// <param name="cast">Cast.</param>
    protected virtual void ConfigureActorsInCast(ICast cast)
    {
      cast.ActorAdded += HandleActorAddedToCast;
      cast.ActorCreated += HandleActorCreatedInCast;
    }

    /// <summary>
    /// Handles the creation of a new actor within an <see cref="ICast"/> instance.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="args">Arguments.</param>
    protected virtual void HandleActorCreatedInCast(object sender, ActorEventArgs args)
    {
      SubscribeReporter(args.Actor);
    }

    /// <summary>
    /// Handles the addition of a new actor to an <see cref="ICast"/> instance.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="args">Arguments.</param>
    protected virtual void HandleActorAddedToCast(object sender, ActorEventArgs args)
    {
      // Intentional no-op, method is here for subclasses to override.
    }

    /// <summary>
    /// Gets the <see cref="Reporting.IReporter"/> from the current context and subscribes it to the given actor.
    /// </summary>
    /// <param name="actor">Actor.</param>
    protected virtual void SubscribeReporter(IActor actor)
    {
      var reporter = Context.GetReporter();
      if(reporter == null)
        return;

      reporter.Subscribe(actor);
    }

    /// <summary>
    /// Informs the reporter (from the current context) that the test run has completed.
    /// </summary>
    protected virtual void InformReporterOfCompletion()
    {
      var reporter = Context.GetReporter();
      if(reporter == null)
        return;

      reporter.CompleteTestRun();
    }

    /// <summary>
    /// Gets the <see cref="Reporting.IReporter"/> from the current context and uses it to write a report.
    /// </summary>
    protected virtual void WriteReport()
    {
      var report = GetReportModel();
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
    protected Report GetReportModel()
    {
      var reporter = Context.GetReportBuildingReporter();
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
    protected virtual void RegisterDefaultWebBrowsingAbility()
    {
      var transformer = GetUriTransformer();
      Context.RegisterSingletonBrowseTheWebAbility(transformer);
    }

    /// <summary>
    /// Disposes the web browsing ability from the current context.
    /// </summary>
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
