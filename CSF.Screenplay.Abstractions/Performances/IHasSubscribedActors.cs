using System;
using System.Collections.Generic;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay
{
    /// <summary>
    /// An object which may subscribe to actor's events.
    /// </summary>
    public interface IHasSubscribedActors
    {
        /// <summary>
        /// Adds a subscription to the specified Actor's events.
        /// </summary>
        /// <param name="actor">The actor to which the current object should subscribe.</param>
        /// <exception cref="ArgumentNullException">If the <paramref name="actor"/> is <see langword="null" />.</exception>
        void SubscribeTo(IHasPerformableEvents actor);

        /// <summary>
        /// Gets a collection of the Actors to whom the current object subscribes.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Please note that the collection returned by this method is a copy of the currently-subscribed actors; a snapshot.
        /// It will not be automatically updated if an actor is newly subscribed-to.
        /// </para>
        /// </remarks>
        /// <returns>A collection of actors</returns>
        IReadOnlyCollection<IHasPerformableEvents> GetSubscribedActors();
    }
}

