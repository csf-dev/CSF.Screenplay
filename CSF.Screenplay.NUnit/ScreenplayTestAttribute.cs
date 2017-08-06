using System;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace CSF.Screenplay.NUnit
{
  /// <summary>
  /// Indicates one or more Screenplay tests.  Typically applied at either the Assembly or TestFixture level,
  /// any tests contained within the affected scope will be treated as screenplay tests.
  /// </summary>
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Assembly,
                  AllowMultiple = true)]
  public class ScreenplayTestAttribute : TestActionAttribute
  {
    /// <summary>
    /// Gets the targets for this attribute (the affected tests).
    /// </summary>
    /// <value>The targets.</value>
    public override ActionTargets Targets => ActionTargets.Test;

    /// <summary>
    /// Executes actions after each affected test.
    /// </summary>
    /// <param name="test">Test.</param>
    public override void AfterTest(ITest test)
    {
      InformReporterOfCompletedScenario();
    }

    /// <summary>
    /// Executes actions before each affected test.
    /// </summary>
    /// <param name="test">Test.</param>
    public override void BeforeTest(ITest test)
    {
      DismissCast();
      InformReporterOfNewScenario(test);
    }

    /// <summary>
    /// Informs the reporter (if registered in the current <see cref="ScreenplayContext"/>) of a completed scenario.
    /// </summary>
    protected virtual void InformReporterOfCompletedScenario()
    {
      var reporter = Context.GetReporter();
      if(reporter == null)
        return;

      var result = TestContext.CurrentContext.Result;
      reporter.CompleteScenario(result.Outcome.Status == TestStatus.Passed);
    }

    /// <summary>
    /// Informs the reporter (if registered in the current <see cref="ScreenplayContext"/>) of a new scenario.
    /// </summary>
    /// <param name="test">The NUnit test object.</param>
    protected virtual void InformReporterOfNewScenario(ITest test)
    {
      var reporter = Context.GetReporter();
      if(reporter == null)
        return;

      var adapter = new ScenarioAdapter(test);
      reporter.BeginNewScenario(adapter.ScenarioId, adapter.ScenarioName, adapter.FeatureName, adapter.FeatureId);
    }

    /// <summary>
    /// Discmisses (clears) the current <see cref="Actors.ICast"/>, if it is registered in the
    /// current <see cref="ScreenplayContext"/>.
    /// </summary>
    protected virtual void DismissCast()
    {
      var cast = Context.GetCast();
      if(cast == null)
        return;
      
      cast.Dismiss();
    }

    /// <summary>
    /// Gets the current screenplay context.
    /// </summary>
    /// <value>The context.</value>
    protected ScreenplayContext Context => ScreenplayContext.Current;
  }
}
