using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CSF.Screenplay.Integration;
using CSF.Screenplay.Scenarios;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Builders;
using NUnit.Framework.Internal.Commands;

namespace CSF.Screenplay.NUnit
{
  /// <summary>
  /// Applied to an assembly, fixture or test - this indicates that a test (or all of the tests within the
  /// scope of this attribute) are Screenplay tests.
  /// </summary>
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
  public class ScreenplayAttribute : Attribute, ITestAction, ITestBuilder
  {
    readonly IntegrationReader integrationReader;

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
      var scenario = ScenarioAdapter.GetScenario(test);
      var integration = integrationReader.GetIntegration(test);
      integration.BeforeScenario(scenario);
    }

    /// <summary>
    /// Performs actions after each test.
    /// </summary>
    /// <param name="test">Test.</param>
    public void AfterTest(ITest test)
    {
      var scenario = ScenarioAdapter.GetScenario(test);
      var outcome = GetOutcome(test);
      var integration = integrationReader.GetIntegration(test);
      integration.AfterScenario(scenario, outcome);
    }

    /// <summary>
    /// Builds a collectio of NUnit test methods from metadata about the test.
    /// </summary>
    /// <returns>The test methods.</returns>
    /// <param name="method">Method information.</param>
    /// <param name="suite">Test suite information.</param>
    public IEnumerable<TestMethod> BuildFrom(IMethodInfo method, Test suite)
    {
      var scenario = CreateScenario(method, suite);
      suite.Properties.Add(ScenarioAdapter.ScreenplayScenarioKey, scenario);
      return BuildFrom(method, suite, scenario);
    }

    bool? GetOutcome(ITest test)
    {
      var success = ScenarioAdapter.GetSuccess(test);
      var failure = ScenarioAdapter.GetFailure(test);

      if(success) return true;
      if(failure) return false;

      return null;
    }

    IEnumerable<TestMethod> BuildFrom(IMethodInfo method, Test suite, IScreenplayScenario scenario)
    {
      var builder = new NUnitTestCaseBuilder();
      var tcParams = new TestCaseParameters(new [] { scenario });

      var testMethod = builder.BuildTestMethod(method, suite, tcParams);

      return new [] { testMethod };
    }

    IScreenplayScenario CreateScenario(IMethodInfo method, Test suite)
    {
      var integration = integrationReader.GetIntegration(method);
      var adapter = new ScenarioAdapter(suite, method, integration);
      return adapter.CreateScenario();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.NUnit.ScreenplayAttribute"/> class.
    /// </summary>
    public ScreenplayAttribute()
    {
      integrationReader = new IntegrationReader();
    }
  }
}
