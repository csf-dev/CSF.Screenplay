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
  public class ScreenplayTestAttribute : Attribute, ITestBuilder, ITestAction
  {
    [ThreadStatic]
    static ScreenplayScenario scenario;

    /// <summary>
    /// Gets the targets for the attribute (when performing before/after test actions).
    /// </summary>
    /// <value>The targets.</value>
    public ActionTargets Targets => ActionTargets.Test;

    //public IEnumerable GetData(IParameterInfo parameter)
    //{
    //  if(parameter.ParameterType == typeof(ScreenplayScenario)
    //     || parameter.ParameterType == typeof(IScreenplayScenario))
    //  {
    //    return new [] { scenario };
    //  }

    //  return null;
    //}

    public IEnumerable<TestMethod> BuildFrom(IMethodInfo method, Test suite)
    {
      var builder = new NUnitTestCaseBuilder();
      var testCaseParams = new TestCaseParameters(new [] { scenario });
      var testMethod = builder.BuildTestMethod(method, suite, testCaseParams);
      return new [] { testMethod };
    }

    /// <summary>
    /// Performs actions before each test.
    /// </summary>
    /// <param name="test">Test.</param>
    public void BeforeTest(ITest test)
    {
      scenario = GetScenario(test);
      CustomiseScenario(scenario);
      scenario.Begin();
    }

    /// <summary>
    /// Performs actions after each test.
    /// </summary>
    /// <param name="test">Test.</param>
    public void AfterTest(ITest test)
    {
      var success = GetScenarioSuccess(test);
      scenario.End(success);
      scenario = null;
    }

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
