using System;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay
{
    /// <summary>A combined registry and factory for <see cref="Actor"/> instances, useful when coordinating multiple
    /// actors across a <see cref="Performance"/></summary>
    /// <remarks>
    /// <para>
    /// The cast is an optional but recommended component of Screenplay logic.
    /// It is used to manage <see cref="Actor"/> objects for the duration of a <see cref="Performance"/>.
    /// Cast objects are always scoped to a <see cref="Performance"/> and have the same lifetime.
    /// Any actors created or tracked by a cast will also automatically share this lifetime.
    /// </para>
    /// <para>
    /// In terms of design patterns, the cast operates as both a registry: <see href="https://martinfowler.com/eaaCatalog/registry.html"/>
    /// and as a factory: <see href="https://en.wikipedia.org/wiki/Factory_method_pattern"/> for actors.
    /// During the cast's lifetime, subsequent calls to an overload of <c>GetActor</c> using the same actor/persona name will
    /// return the instance of <see cref="Actor"/> as was created the first time the method was called with that name.
    /// A cast, and the actors managed by a cast, are independent per <see cref="Performance"/>, though.
    /// </para>
    /// <para>
    /// Performances which use only a single actor might find it simpler to use an <see cref="IPersona"/> directly for the creation
    /// of that single actor, or (not recommended) create the actor manually.
    /// </para>
    /// <para>
    /// In <xref href="ScreenplayGlossaryItem?text=a+Screenplay"/> the cast is <xref href="InjectingServicesArticle?text=a+dependency-injectable+service"/>
    /// which may be used within your performances.
    /// </para>
    /// </remarks>
    /// <seealso cref="IStage"/>
    /// <seealso cref="Actor"/>
    /// <seealso cref="Performance"/>
    /// <seealso cref="IPersona"/>
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