using System;
using System.Reflection;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace CSF.Screenplay.NUnit
{
  /// <summary>
  /// Adapter type for describing a scenario, using NUnit's <c>ITest</c> interface.
  /// </summary>
  public class ScenarioAdapter
  {
    ITest test;

    /// <summary>
    /// Gets the name of the scenario.
    /// </summary>
    /// <value>The name of the scenario.</value>
    public string ScenarioName
    {
      get {
        var method = test.Method?.MethodInfo;

        if(method == null)
          return null;

        return GetDescription(method);
      }
    }

    /// <summary>
    /// Gets the scenario identifier.
    /// </summary>
    /// <value>The scenario identifier.</value>
    public string ScenarioId => test.FullName;

    /// <summary>
    /// Gets the name of the feature.
    /// </summary>
    /// <value>The name of the feature.</value>
    public string FeatureName
    {
      get {
        var fixtureType = GetFixtureType();

        if(fixtureType == null)
          return null;

        return GetDescription(fixtureType);
      }
    }

    /// <summary>
    /// Gets the feature identifier.
    /// </summary>
    /// <value>The feature identifier.</value>
    public string FeatureId
    {
      get {
        var fixtureType = GetFixtureType();

        if(fixtureType == null)
          return null;

        return fixtureType.FullName;
      }
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

    Type GetFixtureType()
    {
      return test.Fixture?.GetType();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.NUnit.ScenarioAdapter"/> class.
    /// </summary>
    /// <param name="test">Test.</param>
    public ScenarioAdapter(ITest test)
    {
      if(test == null)
        throw new ArgumentNullException(nameof(test));

      this.test = test;
    }
  }
}
