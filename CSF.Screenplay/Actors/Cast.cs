using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using CSF.Screenplay.Performances;
using Microsoft.Extensions.DependencyInjection;

namespace CSF.Screenplay.Actors
{
    /// <summary>The default implementation of <see cref="ICast"/> which serves as a registry/factory for <see cref="Actor"/> instances.</summary>
    public sealed class Cast : ICast, IDisposable
    {
        readonly IRelaysPerformanceEvents performanceEventBus;
        readonly ConcurrentDictionary<string,Actor> actors = new ConcurrentDictionary<string,Actor>(StringComparer.InvariantCultureIgnoreCase);

        /// <inheritdoc/>
        public IServiceProvider ServiceProvider { get; }

        /// <inheritdoc/>
        public Guid PerformanceIdentity { get; }

        /// <inheritdoc/>
        public Actor GetActor(string name) => GetActor(new NameOnlyPersona(name));

        /// <inheritdoc/>
        public Actor GetActor(IPersona persona)
        {
            if (persona is null) throw new ArgumentNullException(nameof(persona));
            return actors.GetOrAdd(persona.Name, name => {
                var actor = persona.GetActor(PerformanceIdentity);
                performanceEventBus.InvokeActorCreated(actor);

                // Because the persona creates the actor with abilities already-loaded, we must manually trigger the gain ability event for
                // each ability that they already have.
                foreach (var ability in ((IHasAbilities)actor).Abilities)
                    performanceEventBus.InvokeGainedAbility(actor, ability);
                
                performanceEventBus.SubscribeTo(actor);
                return actor;
            });
        }

        /// <inheritdoc/>
        public IReadOnlyCollection<string> GetCastList() => actors.Keys.ToList();

        /// <inheritdoc/>
        public void Dispose()
        {
            foreach(var actor in actors.Values)
            {
                performanceEventBus.UnsubscribeFrom(actor);
                actor.Dispose();
            }
        }

        /// <summary>Initialises a new instance of <see cref="Cast"/>.</summary>
        /// <param name="serviceProvider">A service provider</param>
        /// <param name="performanceIdentity">The identity of the current performance</param>
        /// <exception cref="ArgumentNullException">If <paramref name="serviceProvider"/> is <see langword="null" />.</exception>
        public Cast(IServiceProvider serviceProvider, Guid performanceIdentity)
        {
            ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            PerformanceIdentity = performanceIdentity;
            performanceEventBus = serviceProvider.GetRequiredService<IRelaysPerformanceEvents>();
        }

        /// <summary>Implementation of <see cref="IPersona"/> which performs no operation beyond giving the actor a name</summary>
        class NameOnlyPersona : IPersona
        {
            /// <inheritdoc/>
            public string Name { get; }

            /// <inheritdoc/>
            public Actor GetActor(Guid performanceIdentity)
                => new Actor(Name, performanceIdentity);

            internal NameOnlyPersona(string name)
            {
                Name = name ?? throw new ArgumentNullException(nameof(name));
            }
        }
    }
}