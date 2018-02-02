﻿using System;
using CSF.FlexDi;
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
    /// Gets the cast from the scenario.
    /// </summary>
    /// <returns>The cast.</returns>
    /// <param name="scenario">Screenplay scenario.</param>
    /// <param name="name">An optional identifying name for the cast instance.</param>
    public static ICast GetCast(this IScenario scenario, string name = null)
    {
      if(scenario == null)
        throw new ArgumentNullException(nameof(scenario));

      return scenario.Resolver.GetCast(name);
    }

    /// <summary>
    /// Gets the cast from the service resolver.
    /// </summary>
    /// <returns>The cast.</returns>
    /// <param name="resolver">Resolver.</param>
    /// <param name="name">Name.</param>
    public static ICast GetCast(this IResolvesServices resolver, string name = null)
    {
      if(resolver == null)
        throw new ArgumentNullException(nameof(resolver));

      return resolver.Resolve<ICast>(name);
    }

    /// <summary>
    /// Gets the stage from the scenario.
    /// </summary>
    /// <returns>The stage.</returns>
    /// <param name="scenario">Screenplay scenario.</param>
    /// <param name="name">An optional identifying name for the stage instance.</param>
    public static IStage GetStage(this IScenario scenario, string name = null)
    {
      if(scenario == null)
        throw new ArgumentNullException(nameof(scenario));

      return scenario.Resolver.GetStage(name);
    }

    /// <summary>
    /// Gets the stage from the service resolver.
    /// </summary>
    /// <returns>The stage.</returns>
    /// <param name="resolver">Resolver.</param>
    /// <param name="name">An optional identifying name for the stage instance.</param>
    public static IStage GetStage(this IResolvesServices resolver, string name = null)
    {
      if(resolver == null)
        throw new ArgumentNullException(nameof(resolver));

      return resolver.Resolve<IStage>(name);
    }
  }
}
