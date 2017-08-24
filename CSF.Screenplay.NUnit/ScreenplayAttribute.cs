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

namespace CSF.Screenplay.NUnit
{
  /// <summary>
  /// Applied to an assembly, fixture or test - this indicates that a test (or all of the tests within the
  /// scope of this attribute) are Screenplay tests.
  /// </summary>
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
  public class ScreenplayAttribute : Attribute, ITestAction, ITestBuilder
  {
    const string ScreenplayScenarioKey = "Current scenario";
    static IScreenplayIntegration integration;
    static object integrationLock;

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
      GetIntegration(test).BeforeScenario(scenario);
    }

    /// <summary>
    /// Performs actions after each test.
    /// </summary>
    /// <param name="test">Test.</param>
    public void AfterTest(ITest test)
    {
      var scenario = GetScenario(test);
      var success = GetScenarioSuccess(test);
      GetIntegration(test).AfterScenario(scenario, success);
    }

    /// <summary>
    /// Builds a collectio of NUnit test methods from metadata about the test.
    /// </summary>
    /// <returns>The test methods.</returns>
    /// <param name="method">Method information.</param>
    /// <param name="suite">Test suite information.</param>
    public IEnumerable<TestMethod> BuildFrom(IMethodInfo method, Test suite)
    {
      GetIntegration(method).LoadIntegration();

      var scenario = CreateScenario(method, suite);

      suite.Properties.Add(ScreenplayScenarioKey, scenario);

      return BuildFrom(method, suite, scenario);
    }

    IEnumerable<TestMethod> BuildFrom(IMethodInfo method, Test suite, ScreenplayScenario scenario)
    {
      var builder = new NUnitTestCaseBuilder();
      var tcParams = new TestCaseParameters(new [] { scenario });

      var testMethod = builder.BuildTestMethod(method, suite, tcParams);

      return new [] { testMethod };
    }

    bool GetScenarioSuccess(ITest test)
    {
      var result = TestContext.CurrentContext.Result;
      return result.Outcome.Status == TestStatus.Passed;
    }

    ScreenplayScenario GetScenario(ITest test)
    {
      var scenario = GetScenario(test.Properties);
      if(scenario != null)
        return scenario;

      scenario = GetScenario(test.Arguments);
      if(scenario != null)
        return scenario;

      var message = $"The test must contain an instance of `{nameof(ScreenplayScenario)}' in " +
                    $"its {nameof(ITest.Properties)} or its {nameof(ITest.Arguments)}.";
      throw new ArgumentException(message, nameof(test));
    }

    ScreenplayScenario GetScenario(IPropertyBag properties)
    {
      if(properties.ContainsKey(ScreenplayScenarioKey))
        return (ScreenplayScenario) properties.Get(ScreenplayScenarioKey);

      return null;
    }

    ScreenplayScenario GetScenario(object[] methodArguments)
    {
      return (ScreenplayScenario) methodArguments.FirstOrDefault(x => x is ScreenplayScenario);
    }

    ScreenplayScenario CreateScenario(IMethodInfo method, Test suite)
    {
      var scenarioAdapter = new ScenarioAdapter(suite, method);
      var featureName = GetFeatureName(scenarioAdapter);
      var scenarioName = GetScenarioName(scenarioAdapter);
      var factory = GetIntegration(method).GetScenarioFactory();

      return factory.GetScenario(featureName, scenarioName);
    }

    IScreenplayIntegration GetIntegration(IMethodInfo method)
    {
      lock(integrationLock)
      {
        if(integration == null)
        {
          var assembly = method?.MethodInfo?.DeclaringType?.Assembly;
          if(assembly == null)
          {
            throw new ArgumentException($"The method must have an associated {nameof(Assembly)}.",
                                      nameof(method));
          }

          var assemblyAttrib = assembly.GetCustomAttribute<ScreenplayAssemblyAttribute>();
          if(assemblyAttrib == null)
          {
            var message = $"All test methods must be contained within assemblies which are " +
              $"decorated with `{nameof(ScreenplayAssemblyAttribute)}'.";
            throw new InvalidOperationException(message);
          }

          integration = assemblyAttrib.Integration;
        }
      }

      return integration;
    }

    IScreenplayIntegration GetIntegration(ITest test)
    {
      if(test.Method == null)
        throw new ArgumentException("The test must specify a method.", nameof(test));

      return GetIntegration(test.Method);
    }

    IdAndName GetFeatureName(ScenarioAdapter test) => new IdAndName(test.FeatureId, test.FeatureName);

    IdAndName GetScenarioName(ScenarioAdapter test) => new IdAndName(test.ScenarioId, test.ScenarioName);

    static ScreenplayAttribute()
    {
      integrationLock = new object();
    }
  }
}
