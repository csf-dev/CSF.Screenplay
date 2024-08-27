using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performances;

namespace CSF.Screenplay
{
    /// <summary>The stage facilitates a contextual <see cref="Actor"/> who is 'in the spotlight' - a currently-active actor</summary>
    /// <remarks>
    /// <para>
    /// The Stage is an optional but recommended component of Screenplay, useful when a <see cref="Performance"/> involves
    /// repeated use of an <see cref="Actor"/>.
    /// It facilitates the use of passive voice and the use of pronouns within the logic of performances without needing to
    /// frequently repeat the <see cref="IHasName.Name"/> of the actor.
    /// </para>
    /// <para>
    /// It is often more consise and easier to understand performances when some of the steps use the passive voice.
    /// In order to do this, there must be a concept which allows us to discern "which actor is acting at the moment".
    /// The stage provides this via the concept of <xref href="SpotlightGlossaryItem?text=a+Spotlight"/>.
    /// Either zero or one actor may be in the spotlight at any given time.
    /// If a new actor is placed in the spotlight then the previous actor is removed from it.
    /// </para>
    /// <para>
    /// Spotlighting an actor facilitates performance steps which use 'the current actor' instead of a specific
    /// named actor.
    /// </para>
    /// <para>
    /// The lifetime of a stage instance is equal to the lifetime of the current <see cref="Performance"/>.
    /// An actor in the spotlight will be consistent across the lifetime of the performance but will be independent
    /// of other performances.
    /// </para>
    /// <para>
    /// In <xref href="ScreenplayGlossaryItem?text=a+Screenplay"/> the stage is <xref href="InjectingServicesArticle?text=a+dependency-injectable+service"/>
    /// which may be used within your performances.
    /// The stage implicitly consumes some functionality from the <see cref="ICast"/>.
    /// If <see cref="Spotlight(IPersona)"/> is used, then the <see cref="Actor"/> to put in the spotlight will
    /// implicitly be retrieved using the cast, via <see cref="ICast.GetActor(IPersona)"/>.
    /// </para>
    /// </remarks>
    /// <example>
    /// <para>
    /// Consider a <see cref="Performance"/> which is based upon the following, which is described in Gherkin BDD syntax:
    /// <see href="https://cucumber.io/docs/gherkin/"/>.
    /// </para>
    /// <code>
    /// Given Jack can wash dishes
    ///   And Jack has filled a basin with hot water
    ///  When Jack washes a dinner plate
    ///  Then Jack should have one clean dinner plate
    /// </code>
    /// <para>
    /// This format of test is perfectly functional, but each <xref href="PerformableGlossaryItem?text=performable+item"/> needed
    /// to be qualified with the actor's name: "Jack".
    /// This could be more human-readable but also more reusable from a code perspective if we had a context of a 'current'
    /// actor, who we could refer to with a pronoun.
    /// With such a concept, our gherkin could read "he has" or "he washes" and could accept any one of a variety of pronouns.
    /// </para>
    /// </example>
    /// <seealso cref="ICast"/>
    public interface IStage
    {
        /// <summary>
        /// Gets the cast to which the current stage is linked.
        /// </summary>
        ICast Cast { get; }

        /// <summary>
        /// Occurs when an actor enters the spotlight.
        /// </summary>
        /// <remarks>
        /// <para>This might mean that an actor who was previously in the spotlight has been removed and replaced.</para>
        /// </remarks>
        event EventHandler<ActorEventArgs> ActorSpotlit;

        /// <summary>
        /// Occurs when an actor is removed from the spotlight because the spotlight has been turned off.
        /// </summary>
        /// <remarks>
        /// <para>This means that there is now no spotlit actor.</para>
        /// </remarks>
        event EventHandler<PerformanceScopeEventArgs> SpotlightTurnedOff;

        /// <summary>Gets the actor which is currently in the spotlight.</summary>
        /// <returns>The actor who has previously been placed in the spotlight, or a <see langword="null" />
        /// reference if there is presently no actor in the spotlight.</returns>
        Actor GetSpotlitActor();

        /// <summary>Places the specified actor into the spotlight, making them 'the current actor' on this stage.</summary>
        /// <remarks>
        /// <para>
        /// A maximum of one actor may be in the spotlight at any time, so if a different actor is already in the spotlight as this
        /// method is used, then they will be implicitly removed and replaced by the specified actor.
        /// The actor who is in the spotlight may be retrieved by calling <see cref="GetSpotlitActor"/>.
        /// </para>
        /// <para>
        /// If the specified actor is already in the spotlight then this method will have no effect, the actor will remain
        /// in the spotlight.
        /// </para>
        /// <para>
        /// If this method results in a new actor entering the spotlight (that is, they were not already in the spotlight)
        /// then the <see cref="ActorSpotlit"/> event will be triggered.
        /// </para>
        /// <para>To remove an actor from the spotlight without replacing them, use <see cref="TurnSpotlightOff"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">If the actor is <see langword="null" />.</exception>
        void Spotlight(Actor actor);

        /// <summary>Places an actor matching the specified persona into the spotlight, making them 'the current actor' on this stage.</summary>
        /// <remarks>
        /// <para>
        /// A maximum of one actor may be in the spotlight at any time, so if a different actor is already in the spotlight as this
        /// method is used, then they will be implicitly removed and replaced by the actor derived from the persona.
        /// The actor who is in the spotlight may be retrieved by calling <see cref="GetSpotlitActor"/>.
        /// </para>
        /// <para>
        /// If actor indicated by the persona is already in the spotlight then this method will have no effect, the actor will remain
        /// in the spotlight.
        /// </para>
        /// <para>
        /// If this method results in a new actor entering the spotlight (that is, they were not already in the spotlight)
        /// then the <see cref="ActorSpotlit"/> event will be triggered.
        /// </para>
        /// <para>
        /// When spotlighting a persona, the actor instance is retrieved from an <see cref="ICast"/> based upon that same persona.
        /// See <see cref="ICast.GetActor(IPersona)"/> for more information.
        /// </para>
        /// <para>To remove an actor from the spotlight without replacing them, use <see cref="TurnSpotlightOff"/>.</para>
        /// <para>
        /// Consider using <see cref="StageExtensions.Spotlight{TPersona}(IStage)"/> instead of this method; the generic version takes
        /// care of resolving the persona instance from dependency injection for you.
        /// </para>
        /// </remarks>
        /// <returns>The actor instance which was placed into the spotlight.</returns>
        /// <exception cref="ArgumentNullException">If the actor is <see langword="null" />.</exception>
        Actor Spotlight(IPersona persona);

        /// <summary>Removes any existing actor from the spotlight, ensuring that no actor is in the spotlight.</summary>
        /// <remarks>
        /// <para>
        /// If this method results in the removal of an actor from the spotlight then the <see cref="SpotlightTurnedOff"/> event
        /// will be triggered. If there was already no actor in the spotlight when this method is executed then it will have no
        /// effect, the spotlight will remain empty, the event noted will not be triggered and this method will return <see langword="null" />.
        /// </para>
        /// </remarks>
        /// <returns>If an actor was previously in the spotlight, and has now been removed, then this method returns that actor;
        /// otherwise it will return a <see langword="null" /> reference.</returns>
        Actor TurnSpotlightOff();
    }
}