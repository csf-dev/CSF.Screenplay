using System;
using System.Collections;
using System.Collections.Generic;
using CSF.Screenplay.Scenarios;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Builders;

namespace CSF.Screenplay.NUnit
{
  /// <summary>
  /// Applied to an assembly, fixture or test - this indicates that a test (or all of the tests within the
  /// scope of this attribute) are Screenplay tests.
  /// </summary>
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Assembly,
                  AllowMultiple = false)]
  public class ScreenplayAttribute : Attribute, ITestAction
  {
    /// <summary>
    /// Gets the targets for the attribute (when performing before/after test actions).
    /// </summary>
    /// <value>The targets.</value>
    public ActionTargets Targets => ActionTargets.Test;

    /// <summary>
    /// Performs actions before each test.
    /// </summary>
    /// <param name="test">Test.</param>
    public void BeforeTest(ITest test)
    {
      ScenarioGetter.Scenario = GetScenario(test);
      CustomiseScenario(ScenarioGetter.Scenario);
      ScenarioGetter.Scenario.Begin();
    }

    /// <summary>
    /// Performs actions after each test.
    /// </summary>
    /// <param name="test">Test.</param>
    public void AfterTest(ITest test)
    {
      if(ScenarioGetter.Scenario == null)
        return;
      
      var success = GetScenarioSuccess(test);
      ScenarioGetter.Scenario.End(success);
      ScenarioGetter.Scenario = null;
    }

    /// <summary>
    /// Provides a hook by which subclasses may customise the Screenplay scenario before a test case is
    /// executed.
    /// </summary>
    /// <param name="scenario">Scenario.</param>
    protected virtual void CustomiseScenario(ScreenplayScenario scenario)
    {
      // Intentional no-op, subclasses may override this to customise the scenario
    }

    bool GetScenarioSuccess(ITest test)
    {
      var result = TestContext.CurrentContext.Result;
      return result.Outcome.Status == TestStatus.Passed;
    }

    ScreenplayScenario GetScenario(ITest test)
    {
      var testAdapter = new ScenarioAdapter(test);
      var featureName = GetFeatureName(testAdapter);
      var scenarioName = GetScenarioName(testAdapter);
      var factory = GetScenarioFactory();

      return factory.GetScenario(featureName, scenarioName);
    }

    IScenarioFactory GetScenarioFactory() => ScreenplayEnvironment.Default.GetScenarioFactory();

    IdAndName GetFeatureName(ScenarioAdapter test) => new IdAndName(test.FeatureId, test.FeatureName);

    IdAndName GetScenarioName(ScenarioAdapter test) => new IdAndName(test.ScenarioId, test.ScenarioName);
  }
}
