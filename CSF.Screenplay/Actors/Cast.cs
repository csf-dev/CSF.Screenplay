using System;
using System.Collections.Generic;
using CSF.FlexDi;

namespace CSF.Screenplay.Actors
{
  /// <summary>
  /// A cast is a container for many actors.  It may be useful in scenarios in which many actors are involved.
  /// </summary>
  /// <remarks>
  /// <para>
  /// There is no need to make use of a <see cref="Cast"/> instance if there is no need.  If you need only use a
  /// single actor then it is far easier to stick with just one.  For testing scenarios in which multiple actors are
  /// involved though, making all of those actor instances available across multiple classes can be difficult.
  /// </para>
  /// <para>
  /// This is where the <see cref="Cast"/> object becomes useful.
  /// </para>
  /// </remarks>
  public class Cast : ICast
  {
    #region fields

    readonly IDictionary<string,IActor> actors;
    readonly Guid scenarioIdentity;
    readonly IResolvesServices resolver;

    #endregion

    #region properties

    /// <summary>
    /// Gets a collection of the currently-available actors.
    /// </summary>
    /// <value>The actors.</value>
    protected IDictionary<string,IActor> Actors => actors;

    #endregion

    #region public API

    /// <summary>
    /// Gets a single actor by their name, creating them if they do not already exist in the cast.
    /// If this operation leads to the creation of a new actor then it will fire both
    /// <see cref="ActorCreated"/> and then <see cref="ActorAdded"/>.
    /// </summary>
    /// <returns>The named actor, which might be a newly-created actor.</returns>
    /// <param name="name">The actor name.</param>
    public IActor Get(string name)
    {
      if(name == null)
        throw new ArgumentNullException(nameof(name));
      
      IActor actor;

      if(Actors.TryGetValue(name, out actor))
        return actor;

      return CreateAndAddActor(name);
    }

    /// <summary>
    /// Gets an <see cref="IActor"/> from the given cast, making use of an <see cref="IPersona"/> type.
    /// </summary>
    /// <typeparam name="TPersona">The persona type.</typeparam>
    public IActor Get<TPersona>() where TPersona : class,IPersona,new()
    {
      return Get(new TPersona());
    }

    /// <summary>
    /// Gets an <see cref="IActor"/> from the given cast, making use of an <see cref="IPersona"/> type.
    /// </summary>
    /// <param name="persona">A persona instance</param>
    public IActor Get(IPersona persona)
    {
      if(persona == null)
        throw new ArgumentNullException(nameof(persona));

      var abilityProvider = persona as IGrantsResolvedAbilities;
      if(abilityProvider == null) return Get(persona.Name);

      IActor actor;

      if(Actors.TryGetValue(persona.Name, out actor))
        return actor;

      actor = CreateAndAddActor(persona.Name);
      abilityProvider.GrantAbilities(actor, resolver);
      return actor;
    }

    #endregion

    #region methods

    IActor CreateAndAddActor(string name)
    {
      var actor = new Actor(name, scenarioIdentity);
      OnActorCreated(actor);

      Actors.Add(name, actor);
      OnActorAdded(actor);

      return actor;
    }

    #endregion

    #region events and invokers

    /// <summary>
    /// An event which is triggered any time a new actor is created by the current cast.
    /// Fires before <see cref="ActorAdded"/>.
    /// </summary>
    public event EventHandler<ActorEventArgs> ActorCreated;

    /// <summary>
    /// Event invoker for the <see cref="ActorCreated"/> event.
    /// </summary>
    /// <param name="actor">Actor.</param>
    protected virtual void OnActorCreated(IActor actor)
    {
      var args = new ActorEventArgs(actor);
      ActorCreated?.Invoke(this, args);
    }

    /// <summary>
    /// An event which is triggered any time a new actor is added to the current cast.
    /// Where an actor is created then added, this event fires after <see cref="ActorCreated"/>.
    /// </summary>
    public event EventHandler<ActorEventArgs> ActorAdded;

    /// <summary>
    /// Event invoker for the <see cref="ActorAdded"/> event.
    /// </summary>
    /// <param name="actor">Actor.</param>
    protected virtual void OnActorAdded(IActor actor)
    {
      var args = new ActorEventArgs(actor);
      ActorAdded?.Invoke(this, args);
    }

    #endregion

    #region constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="Cast"/> class.
    /// </summary>
    /// <param name="scenarioIdentity">The identity of the current scenario.</param>
    /// <param name="resolver">A service resolver instance.</param>
    public Cast(Guid scenarioIdentity, IResolvesServices resolver)
    {
      if(scenarioIdentity == Guid.Empty)
        throw new ArgumentException(Resources.ExceptionFormats.ActorMustHaveAScenarioId, nameof(scenarioIdentity));
      if(resolver == null)
        throw new ArgumentNullException(nameof(resolver));

      this.scenarioIdentity = scenarioIdentity;
      this.resolver = resolver;
      actors = new Dictionary<string,IActor>();
    }

    #endregion
  }
}
