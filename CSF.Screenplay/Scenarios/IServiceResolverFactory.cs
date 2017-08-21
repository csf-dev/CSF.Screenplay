using System;
namespace CSF.Screenplay.Scenarios
{
  /// <summary>
  /// A service which is capable of providing instances of <see cref="IServiceResolver"/>.
  /// </summary>
  public interface IServiceResolverFactory
  {
    /// <summary>
    /// Gets a resolver.
    /// </summary>
    /// <returns>The resolver.</returns>
    IServiceResolver GetResolver();
  }
}
