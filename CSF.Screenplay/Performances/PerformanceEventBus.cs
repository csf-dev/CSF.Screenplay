using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay.Performances
{
    /// <summary>
    /// Implementation of an event bus for performance-related events.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This object should be used as a singleton across the lifetime of a <see cref="Screenplay"/>.
    /// As an event bus object, it is both a subscriber (a sink) which can receive events.
    /// It is also a publisher, which emits events.
    /// </para>
    /// <para>
    /// The purpose of this object is to aggregate events from many instances of <see cref="Performance"/>
    /// and <see cref="Actor"/> over the duration/lifetime of the Screenplay.
    /// This way, consumers have only a single object to which they should subscribe in order to receive
    /// those events.
    /// </para>
    /// <para>
    /// As you will see from the API of this object, the implementations of <see cref="IHasPerformanceEvents"/>
    /// and <see cref="IRelaysPerformanceEvents"/> are not symmetrical.  Many events are published by subscribing
    /// to the events upon an <see cref="Actor"/>.
    /// </para>
    /// </remarks>
    public class PerformanceEventBus : IHasPerformanceEvents, IRelaysPerformanceEvents
    {
        readonly ConcurrentDictionary<Guid,HashSet<Actor>> subscribedActors = new ConcurrentDictionary<Guid,HashSet<Actor>>();

        #region Pub: Screenplay

        /// <inheritdoc/>
        public event EventHandler ScreenplayStarted;

        /// <inheritdoc/>
        public event EventHandler ScreenplayEnded;

        #endregion

        #region Pub: Performances

        /// <inheritdoc/>
        public event EventHandler<PerformanceEventArgs> PerformanceBegun;

        /// <inheritdoc/>
        public event EventHandler<PerformanceFinishedEventArgs> PerformanceFinished;

        #endregion

        #region Pub: Performables

        /// <inheritdoc/>
        public event EventHandler<PerformableEventArgs> BeginPerformable;

        void OnBeginPerformable(object sender, PerformableEventArgs args)
            => BeginPerformable?.Invoke(sender, args);

        /// <inheritdoc/>
        public event EventHandler<PerformableEventArgs> EndPerformable;

        void OnEndPerformable(object sender, PerformableEventArgs args)
            => EndPerformable?.Invoke(sender, args);
        
        /// <inheritdoc/>
        public event EventHandler<PerformableResultEventArgs> PerformableResult;

        void OnPerformableResult(object sender, PerformableResultEventArgs args)
            => PerformableResult?.Invoke(sender, args);
        
        /// <inheritdoc/>
        public event EventHandler<PerformableFailureEventArgs> PerformableFailed;

        void OnPerformableFailed(object sender, PerformableFailureEventArgs args)
            => PerformableFailed?.Invoke(sender, args);

        /// <inheritdoc/>
        public event EventHandler<PerformableAssetEventArgs> RecordAsset;

        void OnRecordsAsset(object sender, PerformableAssetEventArgs args)
            => RecordAsset?.Invoke(sender, args);

        #endregion

        #region Pub: Actors

        /// <inheritdoc/>
        public event EventHandler<GainAbilityEventArgs> GainedAbility;

        void OnGainedAbility(object sender, GainAbilityEventArgs args)
            => GainedAbility?.Invoke(sender, args);

        /// <inheritdoc/>
        public event EventHandler<ActorEventArgs> ActorCreated;

        /// <inheritdoc/>
        public event EventHandler<ActorEventArgs> ActorSpotlit;

        /// <inheritdoc/>
        public event EventHandler<PerformanceScopeEventArgs> SpotlightTurnedOff;

        #endregion

        #region Sub: Actors

        /// <inheritdoc/>
        public void SubscribeTo(Actor actor)
        {
            if (actor is null)
                throw new ArgumentNullException(nameof(actor));

            var actorsForPerformance = subscribedActors.GetOrAdd(((IHasPerformanceIdentity)actor).PerformanceIdentity, _ => new HashSet<Actor>());
            actorsForPerformance.Add(actor);

            actor.BeginPerformable += OnBeginPerformable;
            actor.EndPerformable += OnEndPerformable;
            actor.PerformableResult += OnPerformableResult;
            actor.PerformableFailed += OnPerformableFailed;
            actor.GainedAbility += OnGainedAbility;
            actor.RecordsAsset += OnRecordsAsset;
        }

        /// <inheritdoc/>
        public void UnsubscribeFrom(Actor actor)
        {
            if (actor is null)
                throw new ArgumentNullException(nameof(actor));

            actor.BeginPerformable -= OnBeginPerformable;
            actor.EndPerformable -= OnEndPerformable;
            actor.PerformableResult -= OnPerformableResult;
            actor.PerformableFailed -= OnPerformableFailed;
            actor.GainedAbility -= OnGainedAbility;
            actor.RecordsAsset -= OnRecordsAsset;

            if(!subscribedActors.TryGetValue(((IHasPerformanceIdentity)actor).PerformanceIdentity, out var actorsForPerformance)) return;
            actorsForPerformance.Remove(actor);
        }

        /// <inheritdoc/>
        public void UnsubscribeFromAllActors(Guid performanceIdentity)
        {
            if (!subscribedActors.TryGetValue(performanceIdentity, out var actorsForPerformance)) return;
            // Copy the source collection with ToList because UnsubscribeFrom would modify it; modifying a collection whilst enumerating it is bad!
            // See the remarks on this method, it's quite normal for there to be no actors remaining at the point where this method is called.
            foreach (var actor in actorsForPerformance.ToList())
                UnsubscribeFrom(actor);
        }

        /// <inheritdoc/>
        public void InvokeActorCreated(Actor actor)
            => ActorCreated?.Invoke(this, new ActorEventArgs(actor));

        /// <inheritdoc/>
        public void InvokeGainedAbility(Actor actor, object ability)
            => GainedAbility?.Invoke(this, new GainAbilityEventArgs(actor, ability));

        /// <inheritdoc/>
        public void InvokeActorSpotlit(Actor actor)
            => ActorSpotlit?.Invoke(this, new ActorEventArgs(actor));

        /// <inheritdoc/>
        public void InvokeSpotlightTurnedOff(Guid performanceIdentity)
            => SpotlightTurnedOff?.Invoke(this, new PerformanceScopeEventArgs(performanceIdentity));

        #endregion

        #region Sub: Performances

        /// <inheritdoc/>
        public void InvokePerformanceBegun(IPerformance performance)
            => PerformanceBegun?.Invoke(this, new PerformanceEventArgs(performance));

        /// <inheritdoc/>
        public void InvokePerformanceFinished(IPerformance performance, bool? success)
            => PerformanceFinished?.Invoke(this, new PerformanceFinishedEventArgs(performance, success));

        #endregion

        #region Sub: Screenplay

        /// <inheritdoc/>
        public void InvokeScreenplayStarted() => ScreenplayStarted?.Invoke(this, EventArgs.Empty);

        /// <inheritdoc/>
        public void InvokeScreenplayEnded() => ScreenplayEnded?.Invoke(this, EventArgs.Empty);

        #endregion
    }
}