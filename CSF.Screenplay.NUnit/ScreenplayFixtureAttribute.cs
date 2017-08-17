using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace CSF.Screenplay.NUnit
{
  /// <summary>
  /// Similar to <c>TestFixtureAttribute</c>, but used for marking up test fixtures which contain screenplay tests.
  /// This ensures that they receive the correct screenplay context via their constructor.
  /// </summary>
  public class ScreenplayFixtureAttribute : TestFixtureAttribute, IFixtureBuilder, ITestAction
  {
    readonly ScreenplayFixtureBuilder fixtureBuilder;
    readonly BeforeAfterTestHelper beforeAfterHelper;

    /// <summary>
    /// Gets the targets for the attribute (when performing before/after test actions).
    /// </summary>
    /// <value>The targets.</value>
    public ActionTargets Targets => ActionTargets.Test;

    /// <summary>
    /// Builds the test fixture from type information.
    /// </summary>
    /// <returns>The test suites (representing the fixture).</returns>
    /// <param name="typeInfo">Type info.</param>
    public new IEnumerable<TestSuite> BuildFrom (ITypeInfo typeInfo)
    {
      return fixtureBuilder.BuildTestSuites(typeInfo);
    }

    IEnumerable<TestSuite> IFixtureBuilder.BuildFrom(ITypeInfo typeInfo)
    {
      return fixtureBuilder.BuildTestSuites(typeInfo);
    }

    /// <summary>
    /// Performs actions before each test.
    /// </summary>
    /// <param name="test">Test.</param>
    public void BeforeTest(ITest test)
    {
      var context = ScreenplayContextContainer.GetContext();
      beforeAfterHelper.BeforeScenario(context, test);
    }

    /// <summary>
    /// Performs actions after each test.
    /// </summary>
    /// <param name="test">Test.</param>
    public void AfterTest(ITest test)
    {
      var context = ScreenplayContextContainer.GetContext();
      beforeAfterHelper.AfterScenario(context, test);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.NUnit.ScreenplayFixtureAttribute"/> class.
    /// </summary>
    public ScreenplayFixtureAttribute() : this(new object[0]) {}

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.NUnit.ScreenplayFixtureAttribute"/> class.
    /// </summary>
    /// <param name="arguments">Arguments.</param>
    public ScreenplayFixtureAttribute(params object[] arguments) : base(arguments)
    {
      fixtureBuilder = new ScreenplayFixtureBuilder();
      beforeAfterHelper = new BeforeAfterTestHelper();
    }
  }
}
