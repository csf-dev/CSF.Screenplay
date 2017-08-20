using System;
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
  public class ScreenplayAttribute : Attribute, ITestAction, ITestBuilder
  {
    internal const string ScreenplayScenarioKey = "Current scenario";

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
      var scenario = GetScenario(test);
      CustomiseScenario(scenario);
      scenario.Begin();
    }

    /// <summary>
    /// Performs actions after each test.
    /// </summary>
    /// <param name="test">Test.</param>
    public void AfterTest(ITest test)
    {
      var scenario = GetScenario(test);

      if(scenario == null)
        return;
      
      var success = GetScenarioSuccess(test);
      scenario.End(success);
    }

    public IEnumerable<TestMethod> BuildFrom(IMethodInfo method, Test suite)
    {
      var scenario = CreateScenario(method, suite);

      suite.Properties.Add(ScreenplayScenarioKey, scenario);

      var builder = new NUnitTestCaseBuilder();
      var tcParams = new TestCaseParameters(new [] { scenario });

      var testMethod = builder.BuildTestMethod(method, suite, tcParams);

      return new [] { testMethod };
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

    ServiceRegistry GetRegistry()
    {
      var builder = new ServiceRegistryBuilder();
      RegisterServices(builder);
      return builder.BuildRegistry();
    }

    bool GetScenarioSuccess(ITest test)
    {
      var result = TestContext.CurrentContext.Result;
      return result.Outcome.Status == TestStatus.Passed;
    }

    protected virtual ScreenplayScenario GetScenario(ITest test)
    {
      if(!test.Properties.ContainsKey(ScreenplayScenarioKey))
        return null;

      return (ScreenplayScenario) test.Properties.Get(ScreenplayScenarioKey);
    }

    protected virtual ScreenplayScenario CreateScenario(IMethodInfo method, Test suite)
    {
      var testAdapter = new SuitAndMethodScenarioAdapter(suite, method);
      var featureName = GetFeatureName(testAdapter);
      var scenarioName = GetScenarioName(testAdapter);
      var factory = GetScenarioFactory();

      return factory.GetScenario(featureName, scenarioName);
    }

    /// <summary>
    /// Registers services which will be used by Screenplay.  Subclasses should override this method,
    /// providing the applicable registration code.
    /// </summary>
    /// <param name="builder">Builder.</param>
    protected abstract void RegisterServices(IServiceRegistryBuilder builder);

    IScenarioFactory GetScenarioFactory() => ScreenplayEnvironment.Default.GetScenarioFactory();

    IdAndName GetFeatureName(IScenarioAdapter test) => new IdAndName(test.FeatureId, test.FeatureName);

    IdAndName GetScenarioName(IScenarioAdapter test) => new IdAndName(test.ScenarioId, test.ScenarioName);
  }
}
