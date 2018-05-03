using System;
using System.Collections.Generic;
using CSF.FlexDi;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay
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
  public interface ICast
  {
    /// <summary>
    /// An event which is triggered any time a new actor is created by the current cast.
    /// Fires before <see cref="ActorAdded"/>.
    /// </summary>
    event EventHandler<ActorEventArgs> ActorCreated;

    /// <summary>
    /// An event which is triggered any time a new actor is added to the current cast.
    /// Where an actor is created then added, this event fires after <see cref="ActorCreated"/>.
    /// </summary>
    event EventHandler<ActorEventArgs> ActorAdded;

    /// <summary>
    /// Gets a collection of all of the actors contained within the current instance.
    /// </summary>
    /// <returns>A collection of actors.</returns>
    IEnumerable<IActor> GetAll();

    /// <summary>
    /// Gets a single actor by their name, but does not create a new actor if they do not already exist.
    /// </summary>
    /// <returns>The named actor, or a <c>null</c> reference if no such actor is contained in the current instance.</returns>
    /// <param name="name">The actor name.</param>
    IActor GetExisting(string name);

    /// <summary>
    /// Gets a value which indicates whether or not the current instance has an actor of the given name.
    /// </summary>
    /// <returns><c>true</c>, if an actor of the specified name exists in the current cast, <c>false</c> otherwise.</returns>
    /// <param name="name">The name for which to search.</param>
    bool HasActor(string name);

    /// <summary>
    /// Gets a single actor by their name, creating them if they do not already exist in the cast.
    /// If this operation leads to the creation of a new actor then it will fire both
    /// <see cref="ActorCreated"/> and then <see cref="ActorAdded"/>.
    /// </summary>
    /// <returns>The named actor, which might be a newly-created actor.</returns>
    /// <param name="name">The actor name.</param>
    IActor Get(string name);

    /// <summary>
    /// Gets a single actor by their name, creating them if they do not already exist in the cast.
    /// If this operation leads to the creation of a new actor then it will fire both
    /// <see cref="ActorCreated"/> and then <see cref="ActorAdded"/>.
    /// </summary>
    /// <returns>The named actor, which might be a newly-created actor.</returns>
    /// <param name="name">The actor name.</param>
    /// <param name="createCustomisation">If the actor does not yet exist, then this action will be executed to customise the newly-created actor.</param>
    IActor Get(string name, Action<IActor> createCustomisation);

    /// <summary>
    /// Gets a single actor by their name, creating them if they do not already exist in the cast.
    /// If this operation leads to the creation of a new actor then it will fire both
    /// <see cref="ActorCreated"/> and then <see cref="ActorAdded"/>.
    /// </summary>
    /// <returns>The named actor, which might be a newly-created actor.</returns>
    /// <param name="name">The actor name.</param>
    /// <param name="createCustomisation">If the actor does not yet exist, then this action will be executed to customise the newly-created actor.</param>
    IActor Get(string name, Action<IResolvesServices,IActor> createCustomisation);

    /// <summary>
    /// Gets an <see cref="IActor"/> from the given cast, making use of an <see cref="IPersona"/> type.
    /// </summary>
    /// <typeparam name="TPersona">The persona type.</typeparam>
    IActor Get<TPersona>() where TPersona : class,IPersona,new();

    /// <summary>
    /// Gets an <see cref="IActor"/> from the given cast, making use of an <see cref="IPersona"/> type.
    /// </summary>
    /// <param name="persona">A persona instance</param>
    IActor Get(IPersona persona);

    /// <summary>
    /// Creates a new actor of the given name and adds it to the current cast instance.
    /// This operation will fire both <see cref="ActorCreated"/> and then <see cref="ActorAdded"/>.
    /// </summary>
    /// <param name="name">The actor name.</param>
    void Add(string name);

    /// <summary>
    /// Adds the given actor to the current cast instance.
    /// This operation will fire <see cref="ActorAdded"/> but not <see cref="ActorCreated"/>.
    /// </summary>
    /// <param name="actor">An actor.</param>
    void Add(IActor actor);

    /// <summary>
    /// Clears the current cast.
    /// </summary>
    void Dismiss();
  }
}
