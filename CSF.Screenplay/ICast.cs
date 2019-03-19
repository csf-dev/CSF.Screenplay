using System;
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
    /// Gets a single actor by their name, creating them if they do not already exist in the cast.
    /// If this operation leads to the creation of a new actor then it will fire both
    /// <see cref="ActorCreated"/> and then <see cref="ActorAdded"/>.
    /// </summary>
    /// <returns>The named actor, which might be a newly-created actor.</returns>
    /// <param name="name">The actor name.</param>
    IActor Get(string name);

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
  }
}
