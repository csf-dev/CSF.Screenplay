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
    IScreenplayIntegration integration;

    /// <summary>
    /// Gets the current Screenplay integration.
    /// </summary>
    /// <value>The integration.</value>
    public IScreenplayIntegration Integration => integration;

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
      integration.AfterExecutedLastScenario();
    }

    /// <summary>
    /// Executes actions before any tests in the current assembly.
    /// </summary>
    /// <param name="test">Test.</param>
    public override void BeforeTest(ITest test)
    {
      integration.BeforeExecutingFirstScenario();
    }

    IScreenplayIntegration BuildIntegration(Type integrationType)
    {
      if(integrationType == null)
        throw new ArgumentNullException(nameof(integrationType));
      
      if(!typeof(IScreenplayIntegration).IsAssignableFrom(integrationType))
      {
        throw new ArgumentException($"Integration type must implement `{typeof(IScreenplayIntegration).Name}'.",
                                    nameof(integrationType));
      }

      return (IScreenplayIntegration) Activator.CreateInstance(integrationType);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.NUnit.ScreenplayAssemblyAttribute"/> class.
    /// </summary>
    /// <param name="integrationType">Integration type.</param>
    public ScreenplayAssemblyAttribute(Type integrationType)
    {
      integration = BuildIntegration(integrationType);
    }
  }
}
