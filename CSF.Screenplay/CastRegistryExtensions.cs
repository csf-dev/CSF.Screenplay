using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Scenarios;

namespace CSF.Screenplay
{
  /// <summary>
  /// Extension methods related to registering an <see cref="ICast"/> with the current registry builder.
  /// </summary>
  public static class CastRegistryExtensions
  {
    /// <summary>
    /// Registers a cast with the builder.
    /// </summary>
    /// <param name="builder">Registry builder.</param>
    /// <param name="cast">An cast instance.</param>
    /// <param name="name">An optional identifying name for the cast instance.</param>
    public static void RegisterCast(this IServiceRegistryBuilder builder, ICast cast = null, string name = null)
    {
      if(builder == null)
        throw new ArgumentNullException(nameof(builder));
      
      builder.RegisterPerScenario(GetCastFactory(cast), name);
    }

    static Func<IServiceResolver,ICast> GetCastFactory(ICast cast)
    {
      if(cast != null) return r => cast;
      
      return r => new Cast();
    }
  }
}
