using System;
using System.Collections.Generic;
using CSF.FlexDi.Builders;

namespace CSF.Screenplay.Integration
{
  /// <summary>
  /// Container class representing the registrations to be performed upon the container .
  /// </summary>
  public class ServiceRegistrations
  {
    readonly IList<Action<IRegistrationHelper>> perTestRun, perScenario;

    /// <summary>
    /// Service resgirations which last for the complete Screenplay test run.  These are registered upon the
    /// 'root' container.
    /// </summary>
    /// <value>The per-test-run service registrations.</value>
    public IList<Action<IRegistrationHelper>> PerTestRun => perScenario;

    /// <summary>
    /// Service resgirations which only have a per-scenario lifetime (they are disposed and recreated
    /// with each scenario).  These are registered upon the child containers which are created per-scenario.
    /// </summary>
    /// <value>The per-test-run service registrations.</value>
    public IList<Action<IRegistrationHelper>> PerScenario => perScenario;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Integration.ServiceRegistrations"/> class.
    /// </summary>
    public ServiceRegistrations()
    {
      perTestRun = new List<Action<IRegistrationHelper>>();
      perScenario = new List<Action<IRegistrationHelper>>();
    }
  }
}
