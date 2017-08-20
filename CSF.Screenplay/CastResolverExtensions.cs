using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Scenarios;

namespace CSF.Screenplay
{
  /// <summary>
  /// Extension methods related to the resolving of an <see cref="ICast"/>.
  /// </summary>
  public static class CastResolverExtensions
  {
    /// <summary>
    /// Gets the cast from the current service-resolver.
    /// </summary>
    /// <returns>The cast.</returns>
    /// <param name="resolver">Resolver.</param>
    /// <param name="name">An optional identifying name for the cast instance.</param>
    public static ICast GetCast(this IServiceResolver resolver, string name = null)
    {
      if(resolver == null)
        throw new ArgumentNullException(nameof(resolver));

      return resolver.GetService<ICast>(name);
    }
  }
}
