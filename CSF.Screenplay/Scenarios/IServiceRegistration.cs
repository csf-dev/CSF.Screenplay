using System;
namespace CSF.Screenplay.Scenarios
{
  /// <summary>
  /// The registration of a single service, using a given set of metadata.
  /// </summary>
  public interface IServiceRegistration
  {
    /// <summary>
    /// Gets the registration metadata, by which the service may be identified.
    /// </summary>
    /// <value>The metadata.</value>
    ServiceMetadata Metadata { get; }

    /// <summary>
    /// Gets a lazy-loading instance of the service which is wrapped by the current registration.
    /// </summary>
    /// <returns>The service.</returns>
    /// <param name="resolver">Resolver.</param>
    Lazy<object> GetService(IServiceResolver resolver);
  }
}
