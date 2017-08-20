using System;
namespace CSF.Screenplay.Scenarios
{
  public interface ISingletonServiceResolverFactory
  {
    /// <summary>
    /// Gets a resolver.
    /// </summary>
    /// <returns>The resolver.</returns>
    IServiceResolver GetResolver();
  }
}
