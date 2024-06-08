using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Resources;

namespace CSF.Screenplay
{
    /// <summary>An Actor is the 'driving force' behind Screenplay performances; it most typically represents a person</summary>
    /// <remarks>
    /// <para>
    /// An actor should represent an autonomous or semi-autonomous system, able to act proactively.  In most cases that is a human
    /// being.  An actor might also represent an automated system which acts according to policy, so long as configuring/initating that
    /// system is not a part of the performance.
    /// </para>
    /// </remarks>
    /// <example>
    /// <para>
    /// A good example of an actor, aside from a person, includes task scheduling software which executes logic at specific times of
    /// day, provided the performance is written on the basis that the software was already configured before the performance begins.
    /// Within the context of such a performance, the task scheduler executing its logic at the configured time would appear to be a proactive
    /// autonomous action because nothing else within the scope of the performance prompted it.
    /// Conversely a mouse trap would be a poor example of an actor; a mouse trap acts only reactively. The creature entering the trap
    /// would be the actor in such a performance.
    /// </para>
    /// </example>
    public partial class Actor : IHasName, IHasPerformanceIdentity, IHasServiceProvider
    {
        readonly string name;
        readonly Guid performanceIdentity;
        readonly IServiceProvider serviceProvider;

        /// <summary>Gets the actor's name</summary>
        protected virtual string Name => name;

        string IHasName.Name => Name;

        /// <summary>Gets the unique identity for the performance in which this actor is participating</summary>
        protected virtual Guid PerformanceIdentity => performanceIdentity;

        Guid IHasPerformanceIdentity.PerformanceIdentity => PerformanceIdentity;

        /// <summary>Gets a service resolver instance associated with this actor</summary>
        protected virtual IServiceProvider ServiceProvider => serviceProvider;

        IServiceProvider IHasServiceProvider.ServiceProvider => ServiceProvider;

        /// <summary>Initialises a new instance of <see cref="Actor"/></summary>
        /// <param name="name">The actor's name</param>
        /// <param name="performanceIdentity">A unique identity for the performance</param>
        /// <param name="serviceProvider">An optional service resolver associated with the current actor</param>
        public Actor(string name = null,
                     Guid performanceIdentity = default,
                     IServiceProvider serviceProvider = default)
        {
            this.name = name ?? ReportStrings.UnnamedActor;
            this.performanceIdentity = performanceIdentity;
            this.serviceProvider = serviceProvider;
        }
    }
}