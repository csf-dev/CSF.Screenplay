using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Scenarios;

namespace CSF.Screenplay
{
  public static class CastResolverExtensions
  {
    public static ICast GetCast(this IServiceResolver resolver, string name = null)
    {
      if(resolver == null)
        throw new ArgumentNullException(nameof(resolver));

      return resolver.GetService<ICast>(name);
    }
  }
}
