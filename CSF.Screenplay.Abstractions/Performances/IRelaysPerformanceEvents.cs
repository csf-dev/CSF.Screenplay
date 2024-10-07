using System;
using System.Collections.Generic;

namespace CSF.Screenplay.Performances
{
    /// <summary>
    /// An object which can relay events that relate to a <see cref="IPerformance"/>
    /// </summary>
    /// <remarks>
    /// <para>
    /// This object is used as an event sink; a single point of contact to which many objects may send events.
    /// This allows event consumers to receive events from many origins by subscribing to only a single object.
    /// There should only be a single instance of an object which implements this interface, for the lifetime
    /// of a Screenplay.
    /// </para>
    /// <para>
    /// This type is closely related to <see cref="IHasPerformanceEvents"/>.  This is the event sink and
    /// <c>IHasPerformanceEvents</c> is the publisher of those events. Despite this, their APIs are not symmetrical,
    /// as many of the events published are derived by subscribing to an <see cref="Actor"/> instance.
    /// </para>
    /// </remarks>
    /// <seealso cref="IHasPerformanceEvents"/>
    public interface IRelaysPerformanceEvents
    {
        #region Actors

        /// <summary>
        /// Subscribes to (and relays) events from the specified actor.
        /// </summary>
        /// <param name="actor">The actor to which this relay should subscribe.</param>
        void SubscribeTo(Actor actor);

        /// <summary>
        /// Unsubscribes from events from the specified actor.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method is typically used when the actor instance is about to be disposed, such as at the end of
        /// a <see cref="IPerformance"/>.
        /// </para>
        /// </remarks>
        /// <param name="actor">The actor from which this relay should unsubscribe.</param>
        void UnsubscribeFrom(Actor actor);

        /// <summary>
        /// Unsubscribes from all of the events for all of the actors who are part of the the <see cref="IPerformance"/>,
        /// indicated by its identity.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Using this method is equivalent to calling <see cref="UnsubscribeFrom(Actor)"/> for every <see cref="Actor"/>
        /// which is participating in the specified performance.
        /// </para>
        /// <para>
        /// Use this method when ending a performance, as a convenience to unsubscribe from all of its actors at once.
        /// </para>
        /// </remarks>
        /// <param name="performanceIdentity">The identity of a performance.</param>
        void UnsubscribeFromAllActors(Guid performanceIdentity);
        
        /// <summary>
        /// Invokes an event indicating that a new <see cref="Actor"/> has been created and added to the <see cref="IPerformance"/>.
        /// </summary>
        /// <param name="actor">The actor</param>
        void InvokeActorCreated(Actor actor);

        /// <summary>
        /// Invokes an event indicating that a new <see cref="Actor"/> has gained an ability.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Use this method only when an actor gains one or more abilities before the the <see cref="SubscribeTo(Actor)"/> method
        /// has been used to subscribe to the actor.  Once the actor has been subscribed-to by an implementation of this instance,
        /// their newly-added abilities will automatically be captured.
        /// </para>
        /// <para>
        /// In some circumstances where the actor is fully created and has their abilities granted BEFORE there has been an opportunity
        /// to subscribe to them, then this method is required to retrospectively trigger the abilitiy-granted event.
        /// This is applicable for actors who are created by an <see cref="IPersona"/>, which might grant the actors one or more abilities
        /// immediately, as part of their creation.
        /// </para>
        /// </remarks>
        /// <param name="actor">The actor</param>
        /// <param name="ability">The ability that the actor has gained.</param>
        void InvokeGainedAbility(Actor actor, object ability);

        /// <summary>
        /// Invokes an event indicating that an <see cref="Actor"/> has been placed into the Spotlight of an <see cref="IStage"/>.
        /// </summary>
        /// <param name="actor">The actor</param>
        void InvokeActorSpotlit(Actor actor);

        /// <summary>
        /// Invokes an event indicating that the Spotlight of the <see cref="IStage"/> has been 'turned off'.
        /// </summary>
        /// <param name="performanceIdentity">A unique identifier for the current <see cref="IPerformance"/>.</param>
        void InvokeSpotlightTurnedOff(Guid performanceIdentity);

        #endregion

        #region Performances

        /// <summary>
        /// Invokes an event indicating that a <see cref="IPerformance"/> has begun.
        /// </summary>
        /// <param name="performanceIdentity">The performance identity</param>
        /// <param name="namingHierarchy">The performance's hierarchical name</param>
        void InvokePerformanceBegun(Guid performanceIdentity, IList<IdentifierAndName> namingHierarchy);

        /// <summary>
        /// Invokes an event indicating that a <see cref="IPerformance"/> has finished.
        /// </summary>
        /// <param name="performanceIdentity">The performance identity</param>
        /// <param name="namingHierarchy">The performance's hierarchical name</param>
        /// <param name="success">A value indicating whether or not the performance was a success</param>
        void InvokePerformanceFinished(Guid performanceIdentity, IList<IdentifierAndName> namingHierarchy, bool? success);

        #endregion

        #region Screenplay

        /// <summary>
        /// Invokes an event indicating that a Screenplay has started.
        /// </summary>
        void InvokeScreenplayStarted();

        /// <summary>
        /// Invokes an event indicating that a Screenplay has ended.
        /// </summary>
        void InvokeScreenplayEnded();

        #endregion
    }
}