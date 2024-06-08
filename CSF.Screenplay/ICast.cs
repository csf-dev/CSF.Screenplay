using System;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay
{
    /// <summary>A cast is a combined registry and factory for actor instances, useful when coordinating multiple actors across a performance</summary>
    /// <remarks>
    /// <para>
    /// Screenplay performances do not require the use of a Cast, it is an optional technique provided for convenience.
    /// Performances which use only a single actor may find it simpler to use an <see cref="IPersona"/> directly for the creation
    /// of that single actor, or (not recommended) create the actor manually.
    /// </para>
    /// <para>
    /// Implementations of Cast have a lifetime equal to the lifetime of the current performance and retain a registry of the actors
    /// which have been created/used during the lifetime of that performance.
    /// During that lifetime, multiple calls to a <c>GetActor</c> method using the same actor name (or persona-name) will return the
    /// same actor instance without creating a new actor each time.
    /// The actors 'in a cast' will be independent across different performances, however.
    /// </para>
    /// </remarks>
    /// <seealso cref="IStage"/>
    public interface ICast : IHasServiceProvider, IHasPerformanceIdentity
    {
        /// <summary>
        /// Occurs when a new actor is created in the cast.
        /// </summary>
        event EventHandler<ActorEventArgs> ActorCreated;

        /// <summary>
        /// Gets a single <see cref="Actor"/> by their name, creating them if they do not already exist in the cast.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method will create the actor within the current cast, if they do not already exist.
        /// Alternatively, this method will return the existing actor, if they already exist in the cast.
        /// If executing this method results in the creation of a new actor then the <see cref="ActorCreated"/>
        /// event will be triggered.
        /// </para>
        /// <para>
        /// Actor names are matched using a case-insensitive invariant culture string comparison. Cast implementations
        /// should match an existing actor if the specified name differs only in case.
        /// </para>
        /// <para>
        /// If you make use of a same-named actor across multiple performances then it is highly recommended to use personas
        /// in order to consistently define the actor's attributes and abilities. You would then use the overload of this
        /// method which uses that persona to define the actor.
        /// </para>
        /// </remarks>
        /// <returns>An actor of the specified name, either an existing instance or a newly-created actor.</returns>
        /// <param name="name">The name of the actor to get</param>
        /// <seealso cref="IPersona"/>
        Actor GetActor(string name);

        /// <summary>
        /// Gets a single <see cref="Actor"/> based upon a persona, creating them if they do not already exist in the cast.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method will create the actor within the current cast, using the persona as a factory, if they do not already exist.
        /// Alternatively, this method will return the existing actor, if they already exist in the cast, matched using the
        /// <see cref="IPersona"/>'s <see cref="IHasName.Name"/>.
        /// If executing this method results in the creation of a new actor then the <see cref="ActorCreated"/>
        /// event will be triggered.
        /// </para>
        /// <para>
        /// Actor names are matched using a case-insensitive invariant culture string comparison. Cast implementations
        /// should match an existing actor if the specified persona name differs only in case.
        /// </para>
        /// </remarks>
        /// <returns>An actor of the specified name, either an existing instance or a newly-created actor.</returns>
        /// <param name="persona">The persona from which to get an actor</param>
        /// <seealso cref="IPersona"/>
        Actor GetActor(IPersona persona);
    }
}