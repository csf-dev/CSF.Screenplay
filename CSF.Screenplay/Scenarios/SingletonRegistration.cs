using System;
namespace CSF.Screenplay.Scenarios
{
  /// <summary>
  /// Implementation of a <see cref="IServiceRegistration"/> which represents a singleton instance which is used
  /// across all scenarios of a test run.
  /// </summary>
  public class SingletonRegistration : IServiceRegistration
  {
    /// <summary>
    /// Gets the registration metadata, by which the service may be identified.
    /// </summary>
    /// <value>The metadata.</value>
    public ServiceMetadata Metadata { get; private set; }

    /// <summary>
    /// Gets a reference to the service instance.
    /// </summary>
    /// <value>The service.</value>
    public object Service { get; private set; }

    /// <summary>
    /// Gets a lazy-loading instance of the service which is wrapped by the current registration.
    /// </summary>
    /// <returns>The service.</returns>
    /// <param name="resolver">Resolver.</param>
    public Lazy<object> GetService(IServiceResolver resolver)
    {
      return new Lazy<object>(() => Service);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Scenarios.SingletonRegistration"/> class.
    /// </summary>
    /// <param name="metadata">Metadata.</param>
    /// <param name="service">Service.</param>
    public SingletonRegistration(ServiceMetadata metadata, object service)
    {
      if(metadata == null)
        throw new ArgumentNullException(nameof(metadata));

      Metadata = metadata;
      Service = service;
    }
  }
}
