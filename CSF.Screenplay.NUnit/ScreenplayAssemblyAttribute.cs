using System;
using CSF.Screenplay.Scenarios;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace CSF.Screenplay.NUnit
{
  /// <summary>
  /// Indicates that the assembly contains Screenplay tests.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
  public abstract class ScreenplayAssemblyAttribute : TestActionAttribute
  {
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
      Environment.NotifyCompleteTestRun();
    }

    /// <summary>
    /// Executes actions before each affected test.
    /// </summary>
    /// <param name="test">Test.</param>
    public override void BeforeTest(ITest test)
    {
      Environment.ServiceRegistry = GetRegistry();
      RegisterBeforeAndAfterTestRunEvents(Environment);
      Environment.NotifyBeginTestRun();
    }

    ServiceRegistry GetRegistry()
    {
      var builder = new ServiceRegistryBuilder();
      RegisterServices(builder);
      return builder.BuildRegistry();
    }

    ScreenplayEnvironment Environment => ScreenplayEnvironment.Default;

    protected abstract void RegisterServices(IServiceRegistryBuilder builder);

    protected virtual void RegisterBeforeAndAfterTestRunEvents(IProvidesTestRunEvents testRunEvents)
    {
      // Intentional no-op, subclasses may override this to provide functionality.
    }
  }
}
