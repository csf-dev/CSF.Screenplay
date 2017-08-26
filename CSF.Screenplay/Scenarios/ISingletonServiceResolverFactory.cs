using System;
namespace CSF.Screenplay.Scenarios
{
  /// <summary>
  /// Factory type which provides <see cref="IServiceResolver"/> instances.
  /// </summary>
  public interface ISingletonServiceResolverFactory
  {
    /// <summary>
    /// Gets a resolver.
    /// </summary>
    /// <returns>The resolver.</returns>
    IServiceResolver GetSingletonResolver();
  }
}
