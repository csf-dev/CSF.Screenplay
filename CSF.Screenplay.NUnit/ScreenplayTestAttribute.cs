using System;
using CSF.Screenplay.Reporting;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System.Reflection;

namespace CSF.Screenplay.NUnit
{
  /// <summary>
  /// Indicates one or more Screenplay tests.  Typically applied at either the Assembly or TestFixture level,
  /// any tests contained within the affected scope will be treated as screenplay tests.
  /// </summary>
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Assembly,
                  AllowMultiple = true)]
  public class ScreenplayTestAttribute : Attribute, ITestAction
  {
    /// <summary>
    /// Gets the targets for this attribute (the affected tests).
    /// </summary>
    /// <value>The targets.</value>
    public ActionTargets Targets => ActionTargets.Test;

    /// <summary>
    /// Executes actions after each affected test.
    /// </summary>
    /// <param name="test">Test.</param>
    public void AfterTest(ITest test)
    {
      var passed = TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed;
      Stage.Reporter.CompleteScenario(passed);
    }

    /// <summary>
    /// Executes actions before each affected test.
    /// </summary>
    /// <param name="test">Test.</param>
    public void BeforeTest(ITest test)
    {
      HireNewCast();
      InformReporterOfNewScenario(test);
    }

    void InformReporterOfNewScenario(ITest test)
    {
      var testId = test.FullName;
      var testName = GetTestFriendlyName(test);
      string featureName = GetFeatureName(test);

      Stage.Reporter.BeginNewScenario(testId, testName, featureName);
    }

    string GetTestFriendlyName(ITest test)
    {
      var method = test.Method?.MethodInfo;
      if(method != null)
      {
        var descriptionAttrib = method.GetCustomAttribute<DescriptionAttribute>();
        var name = descriptionAttrib?.Properties?.Get("Description")?.ToString();
        if(name != null)
          return name;
      }

      return test.Name;
    }

    string GetFeatureName(ITest test)
    {
      var fixtureType = test.Fixture?.GetType();
      if(fixtureType != null)
      {
        var descriptionAttrib = fixtureType.GetCustomAttribute<DescriptionAttribute>();
        return descriptionAttrib?.Properties?.Get("Description")?.ToString() ?? fixtureType.Name;
      }

      return null;
    }

    void HireNewCast()
    {
      Stage.Cast.Clear();
    }
  }
}
