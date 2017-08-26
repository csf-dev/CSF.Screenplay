using System;
using CSF.Screenplay.Integration;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace CSF.Screenplay.NUnit
{
  /// <summary>
  /// Indicates that the assembly contains Screenplay tests.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
  public class ScreenplayAssemblyAttribute : TestActionAttribute
  {
    static Lazy<IScreenplayIntegration> integration;

    /// <summary>
    /// Gets the current Screenplay integration.
    /// </summary>
    /// <value>The integration.</value>
    public IScreenplayIntegration Integration => integration.Value;

    /// <summary>
    /// Gets the targets for this attribute (the affected tests).
    /// </summary>
    /// <value>The targets.</value>
    public override ActionTargets Targets => ActionTargets.Suite;

    /// <summary>
    /// Executes actions after any tests in the current assembly.
    /// </summary>
    /// <param name="test">Test.</param>
    public override void AfterTest(ITest test)
    {
      Integration.AfterExecutedLastScenario();
    }

    /// <summary>
    /// Executes actions before any tests in the current assembly.
    /// </summary>
    /// <param name="test">Test.</param>
    public override void BeforeTest(ITest test)
    {
      Integration.BeforeExecutingFirstScenario();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.NUnit.ScreenplayAssemblyAttribute"/> class.
    /// </summary>
    /// <param name="configType">Integration type.</param>
    public ScreenplayAssemblyAttribute(Type configType)
    {
      integration = new Lazy<IScreenplayIntegration>(() => new IntegrationFactory().Create(configType));
    }
  }
}
