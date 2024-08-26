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
    /// </remarks>
    public class PerformanceEventBus : IHasPerformanceEvents, IRelaysPerformanceEvents
    {
        readonly ConcurrentDictionary<Guid,HashSet<Actor>> subscribedActors = new ConcurrentDictionary<Guid,HashSet<Actor>>();

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
        public event EventHandler<GainAbilityEventArgs> GainedAbility;

        void OnGainedAbility(object sender, GainAbilityEventArgs args)
            => GainedAbility?.Invoke(sender, args);

        /// <inheritdoc/>
        public event EventHandler<PerformanceEventArgs> PerformanceBegun;

        /// <inheritdoc/>
        public event EventHandler<PerformanceFinishedEventArgs> PerformanceFinished;

        /// <inheritdoc/>
        public void InvokePerformanceBegun(Guid performanceIdentity, IList<IdentifierAndName> namingHierarchy)
        {
            PerformanceBegun?.Invoke(this, new PerformanceEventArgs(performanceIdentity, namingHierarchy?.ToList() ?? new List<IdentifierAndName>()));
        }

        /// <inheritdoc/>
        public void InvokePerformanceFinished(Guid performanceIdentity, IList<IdentifierAndName> namingHierarchy, bool? success)
        {
            PerformanceFinished?.Invoke(this, new PerformanceFinishedEventArgs(performanceIdentity, namingHierarchy?.ToList() ?? new List<IdentifierAndName>(), success));
        }

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

            if(!subscribedActors.TryGetValue(((IHasPerformanceIdentity)actor).PerformanceIdentity, out var actorsForPerformance)) return;
            actorsForPerformance.Remove(actor);
        }

        /// <inheritdoc/>
        public void UnsubscribeFromAllActors(Guid performanceIdentity)
        {
            if (!subscribedActors.TryGetValue(performanceIdentity, out var actorsForPerformance)) return;
            // Copy the source collection with ToList because UnsubscribeFrom would modify it; modifying a collection whilst enumerating it is bad!
            foreach (var actor in actorsForPerformance.ToList())
                UnsubscribeFrom(actor);
        }
    }
}