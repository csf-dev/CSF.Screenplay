using System;
using System.Collections.Generic;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay.Performances
{
    /// <summary>
    /// An object which can relay events that relate to a <see cref="Performance"/>
    /// </summary>
    /// <remarks>
    /// <para>
    /// This object is used as an event sink; a single point of contact to which many objects may send events.
    /// This allows event consumers to receive events from many origins by subscribing to only a single object.
    /// </para>
    /// </remarks>
    public interface IRelaysPerformanceEvents
    {
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
        /// a <see cref="Performance"/>.
        /// </para>
        /// </remarks>
        /// <param name="actor">The actor from which this relay should unsubscribe.</param>
        void UnsubscribeFrom(Actor actor);

        /// <summary>
        /// Unsubscribes from all of the events for all of the actors who are part of the the <see cref="Performance"/>,
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
        /// Invokes an event indicating that a <see cref="Performance"/> has begun.
        /// </summary>
        /// <param name="performanceIdentity">The performance identity</param>
        /// <param name="namingHierarchy">The performance's hierarchical name</param>
        void InvokePerformanceBegun(Guid performanceIdentity, IList<IdentifierAndName> namingHierarchy);

        /// <summary>
        /// Invokes an event indicating that a <see cref="Performance"/> has finished.
        /// </summary>
        /// <param name="performanceIdentity">The performance identity</param>
        /// <param name="namingHierarchy">The performance's hierarchical name</param>
        /// <param name="success">A value indicating whether or not the performance was a success</param>
        void InvokePerformanceFinished(Guid performanceIdentity, IList<IdentifierAndName> namingHierarchy, bool? success);
    }
}