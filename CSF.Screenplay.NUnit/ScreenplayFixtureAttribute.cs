using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace CSF.Screenplay.NUnit
{
  public class ScreenplayFixtureAttribute : TestFixtureAttribute, IFixtureBuilder, ITestAction
  {
    readonly ScreenplayFixtureBuilder fixtureBuilder;
    readonly BeforeAfterTestHelper beforeAfterHelper;

    public ActionTargets Targets => ActionTargets.Test;

    public new IEnumerable<TestSuite> BuildFrom (ITypeInfo typeInfo)
    {
      return fixtureBuilder.BuildTestSuites(typeInfo);
    }

    IEnumerable<TestSuite> IFixtureBuilder.BuildFrom(ITypeInfo typeInfo)
    {
      return fixtureBuilder.BuildTestSuites(typeInfo);
    }

    public void BeforeTest(ITest test)
    {
      var context = ScreenplayContextContainer.GetContext(test.Fixture);
      beforeAfterHelper.BeforeScenario(context, test);
    }

    public void AfterTest(ITest test)
    {
      var context = ScreenplayContextContainer.GetContext(test.Fixture);
      beforeAfterHelper.AfterScenario(context, test);
    }

    public ScreenplayFixtureAttribute() : this(new object[0]) {}

    public ScreenplayFixtureAttribute(params object[] arguments) : base(arguments)
    {
      fixtureBuilder = new ScreenplayFixtureBuilder();
      beforeAfterHelper = new BeforeAfterTestHelper();
    }
  }
}
