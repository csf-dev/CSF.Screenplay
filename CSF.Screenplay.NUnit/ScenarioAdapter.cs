using System;
using System.Linq;
using System.Reflection;
using CSF.Screenplay.Integration;
using CSF.Screenplay.Scenarios;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace CSF.Screenplay.NUnit
{
  /// <summary>
  /// Adapter type which provides a simple API for getting feature/scenario identification information.
  /// </summary>
  public class ScenarioAdapter
  {
    internal const string ScreenplayScenarioKey = "Current scenario";

    readonly ITest featureSuite;
    readonly IMethodInfo scenarioMethod;
    readonly IScreenplayIntegration integration;

    /// <summary>
    /// Gets the name of the scenario.
    /// </summary>
    /// <value>The name of the scenario.</value>
    public string ScenarioName
    {
      get {
        var method = scenarioMethod.MethodInfo;

        if(method == null)
          return null;

        return GetDescription(method);
      }
    }

    /// <summary>
    /// Gets the scenario identifier.
    /// </summary>
    /// <value>The scenario identifier.</value>
    public string ScenarioId => $"{FeatureId}.{scenarioMethod.Name}";

    /// <summary>
    /// Gets the name of the feature.
    /// </summary>
    /// <value>The name of the feature.</value>
    public string FeatureName
    {
      get {
        var fixtureType = scenarioMethod.MethodInfo.DeclaringType;

        if(fixtureType == null)
          return null;

        return GetDescription(fixtureType);
      }
    }

    /// <summary>
    /// Gets the feature identifier.
    /// </summary>
    /// <value>The feature identifier.</value>
    public string FeatureId => featureSuite.FullName;

    /// <summary>
    /// Creates a new Screenplay scenario using the state of the current instance, and the given integration.
    /// </summary>
    /// <returns>The scenario.</returns>
    public IScenario CreateScenario()
    {
      var factory = integration.GetScenarioFactory();
      return factory.GetScenario(FeatureIdAndName, ScenarioIdAndName);
    }

    string GetDescription(MemberInfo member)
    {
      var attrib = member.GetCustomAttribute<DescriptionAttribute>();

      if(attrib == null)
        return member.Name;

      var prop = attrib.Properties.Get("Description");
      if(prop == null)
        return member.Name;

      return prop.ToString();
    }

    IdAndName FeatureIdAndName => new IdAndName(FeatureId, FeatureName);

    IdAndName ScenarioIdAndName => new IdAndName(ScenarioId, ScenarioName);

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.NUnit.ScenarioAdapter"/> class.
    /// </summary>
    /// <param name="featureSuite">Feature suite.</param>
    /// <param name="scenarioMethod">Scenario method.</param>
    /// <param name="integration">Screenplay integration.</param>
    public ScenarioAdapter(ITest featureSuite, IMethodInfo scenarioMethod, IScreenplayIntegration integration)
    {
      if(integration == null)
        throw new ArgumentNullException(nameof(integration));
      if(scenarioMethod == null)
        throw new ArgumentNullException(nameof(scenarioMethod));
      if(featureSuite == null)
        throw new ArgumentNullException(nameof(featureSuite));
      
      this.scenarioMethod = scenarioMethod;
      this.featureSuite = featureSuite;
      this.integration = integration;
    }

    /// <summary>
    /// Gets a value which indicates whether a single test passed.
    /// </summary>
    /// <returns><c>true</c>, if the test was successful, <c>false</c> otherwise.</returns>
    /// <param name="test">Test.</param>
    public static bool GetSuccess(ITest test)
    {
      var result = TestContext.CurrentContext.Result;
      return result.Outcome.Status == TestStatus.Passed;
    }

    /// <summary>
    /// Gets a value which indicates whether a single test failed.
    /// </summary>
    /// <returns><c>true</c>, if the test was a failure, <c>false</c> otherwise.</returns>
    /// <param name="test">Test.</param>
    public static bool GetFailure(ITest test)
    {
      var result = TestContext.CurrentContext.Result;
      return result.Outcome.Status == TestStatus.Failed;
    }

    /// <summary>
    /// Gets an <see cref="IScenario"/> from a given NUnit test instance.
    /// </summary>
    /// <returns>The scenario.</returns>
    /// <param name="test">Test.</param>
    public static IScenario GetScenario(ITest test)
    {
      var scenario = GetScenario(test.Properties);
      if(scenario != null)
        return scenario;

      var message = String.Format(Resources.ExceptionFormats.TestMustHaveAScenarioInProperties,
                                  nameof(IScenario));
      throw new ArgumentException(message, nameof(test));
    }

    /// <summary>
    /// Gets an <see cref="IScenario"/> from a given NUnit property bad.
    /// </summary>
    /// <returns>The scenario.</returns>
    /// <param name="properties">Properties.</param>
    public static IScenario GetScenario(IPropertyBag properties)
    {
      if(properties.ContainsKey(ScreenplayScenarioKey))
        return (IScenario) properties.Get(ScreenplayScenarioKey);

      return null;
    }

    /// <summary>
    /// Gets an <see cref="IScenario"/> from the arguments to an NUnit test method.
    /// </summary>
    /// <returns>The scenario.</returns>
    /// <param name="methodArguments">Method arguments.</param>
    public static IScenario GetScenario(object[] methodArguments)
    {
      return (IScenario) methodArguments.FirstOrDefault(x => x is IScenario);
    }
  }
}
