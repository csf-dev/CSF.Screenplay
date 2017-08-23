using System;
using System.Collections.Generic;
using CSF.Screenplay.NUnit;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Builders;

namespace CSF.Screenplay.Web.Tests
{
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Assembly,
                  AllowMultiple = false)]
  public class ScreenplayScenarioAttribute : Attribute, ITestBuilder
  {
    public IEnumerable<TestMethod> BuildFrom(IMethodInfo method, Test suite)
    {
      var builder = new NUnitTestCaseBuilder();
      var tcParams = new TestCaseParameters(new [] { ScenarioGetter.Scenario });
      var test = builder.BuildTestMethod(method, suite, tcParams);
      return new [] { test };
    }
  }
}
