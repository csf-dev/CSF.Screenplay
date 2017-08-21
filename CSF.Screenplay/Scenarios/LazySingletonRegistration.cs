using System;
namespace CSF.Screenplay.Scenarios
{
  public class LazySingletonRegistration : IServiceRegistration
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
    public Lazy<object> Service { get; private set; }

    /// <summary>
    /// Gets a lazy-loading instance of the service which is wrapped by the current registration.
    /// </summary>
    /// <returns>The service.</returns>
    /// <param name="resolver">Resolver.</param>
    public Lazy<object> GetService(IServiceResolver resolver) => Service;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Scenarios.SingletonRegistration"/> class.
    /// </summary>
    /// <param name="metadata">Metadata.</param>
    /// <param name="initialiser">An initialiser function for the service.</param>
    public LazySingletonRegistration(ServiceMetadata metadata, Func<object> initialiser)
    {
      if(metadata == null)
        throw new ArgumentNullException(nameof(metadata));
      if(initialiser == null)
        throw new ArgumentNullException(nameof(initialiser));

      Metadata = metadata;
      Service = new Lazy<object>(() => initialiser);
    }
  }
}
