using System;
using System.Collections.Generic;

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
    /// Gets a single actor by their name.
    /// </summary>
    /// <returns>The named actor, or a <c>null</c> reference if no such actor is contained in the current instance.</returns>
    /// <param name="name">The actor name.</param>
    IActor GetActor(string name);

    /// <summary>
    /// Gets a single actor by their name, creating them if they do not already exist in the cast.
    /// If this operation leads to the creation of a new actor then it will fire both
    /// <see cref="ActorCreated"/> and then <see cref="ActorAdded"/>.
    /// </summary>
    /// <returns>The named actor, which might be a newly-created actor.</returns>
    /// <param name="name">The actor name.</param>
    IActor GetOrCreate(string name);

    /// <summary>
    /// Creates a new actor of the given name, adds it to the current cast instance and returns it.
    /// This operation will fire both <see cref="ActorCreated"/> and then <see cref="ActorAdded"/>.
    /// </summary>
    /// <returns>The created actor.</returns>
    /// <param name="name">The actor name.</param>
    IActor CreateAndAdd(string name);

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
