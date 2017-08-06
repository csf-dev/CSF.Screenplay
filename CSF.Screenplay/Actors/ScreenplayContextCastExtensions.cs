using System;
using CSF.Screenplay.Context;

namespace CSF.Screenplay.Actors
{
  /// <summary>
  /// Extension methods related to registering and retrieving the <see cref="ICast"/> associated with the current
  /// context.
  /// </summary>
  public static class ScreenplayContextCastExtensions
  {
    /// <summary>
    /// Registers the cast with the context.  This cast instance is used for all test cases/scenarios in the test run.
    /// </summary>
    /// <param name="context">Context.</param>
    /// <param name="cast">Cast.</param>
    /// <param name="name">An optional identifying name for the cast instance.</param>
    public static void RegisterCast(this IScreenplayContext context, ICast cast, string name = null)
    {
      if(context == null)
        throw new ArgumentNullException(nameof(context));
      if(cast == null)
        throw new ArgumentNullException(nameof(cast));
      
      context.RegisterSingleton(cast, name);
    }

    /// <summary>
    /// Adds a registration to the screenplay context for a default <see cref="Cast"/> instance, registered for all
    /// test cases/scenarios in the test run.
    /// </summary>
    /// <returns>The created cast instance.</returns>
    /// <param name="context">Context.</param>
    public static ICast RegisterDefaultCast(this IScreenplayContext context)
    {
      if(context == null)
        throw new ArgumentNullException(nameof(context));

      var cast = new Cast();
      RegisterCast(context, cast);
      return cast;
    }

    /// <summary>
    /// Gets the previously-registered cast.
    /// </summary>
    /// <returns>The cast.</returns>
    /// <param name="context">Context.</param>
    /// <param name="name">An optional identifying name for the cast instance.</param>
    public static ICast GetCast(this IScreenplayContext context, string name = null)
    {
      if(context == null)
        throw new ArgumentNullException(nameof(context));

      return context.GetService<ICast>(name);
    }
  }
}
