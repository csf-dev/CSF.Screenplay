using System;
namespace CSF.Screenplay.Scenarios
{
  /// <summary>
  /// Implementation of a <see cref="IServiceRegistration"/> which represents a per-scenario service,
  /// which is constructed (from a factory method) once for each scenario within the test run.
  /// </summary>
  public class PerScenarioRegistration : IServiceRegistration
  {
    /// <summary>
    /// Gets the registration metadata, by which the service may be identified.
    /// </summary>
    /// <value>The metadata.</value>
    public ServiceMetadata Metadata { get; private set; }

    /// <summary>
    /// Gets the service factory function.
    /// </summary>
    /// <value>The service factory.</value>
    public Func<IScreenplayScenario,object> ServiceFactory { get; private set; }

    /// <summary>
    /// Gets a lazy-loading instance of the service which is wrapped by the current registration.
    /// </summary>
    /// <returns>The service.</returns>
    /// <param name="resolver">Resolver.</param>
    public Lazy<object> GetService(IServiceResolver resolver)
    {
      return new Lazy<object>(() => ServiceFactory((IScreenplayScenario) resolver));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Scenarios.PerScenarioRegistration"/> class.
    /// </summary>
    /// <param name="metadata">Metadata.</param>
    /// <param name="factory">Factory.</param>
    public PerScenarioRegistration(ServiceMetadata metadata, Func<IScreenplayScenario,object> factory)
    {
      if(metadata == null)
        throw new ArgumentNullException(nameof(metadata));
      if(factory == null)
        throw new ArgumentNullException(nameof(factory));

      Metadata = metadata;
      ServiceFactory = factory;
    }
  }
}
