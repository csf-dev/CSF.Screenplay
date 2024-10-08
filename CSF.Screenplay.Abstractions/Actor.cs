using System;
using CSF.Screenplay.Resources;

namespace CSF.Screenplay
{
    /// <summary>A representation of an autonomous, or at least seemingly-autonomous, person or system which directs the events of a <see cref="IPerformance"/>.</summary>
    /// <remarks>
    /// <para>
    /// An actor should represent an autonomous or semi-autonomous system.  Most commonly is a human being using a piece of software.
    /// An actor might also represent an automated system which acts according to policy, so long as configuring/initating that
    /// system is not a part of the <see cref="IPerformance"/>.
    /// Within a performance, <xref href="PerformableGlossaryItem?text=each+performable+item"/> is executed in the context of the actor
    /// which performed it.
    /// </para>
    /// <para>
    /// Actors <xref href="AbilityGlossaryItem?text=have+abilities"/> which provide them the means by which to interact with the software.
    /// </para>
    /// <para>
    /// A single conceptual actor must be represented by a single instance of this type; two actors of the same name are not considered to
    /// be the same actor, nor will they behave as such.
    /// Instances of actor should have an object lifetime which lasts for the remainder of the performance in which they were created.
    /// Instances of actor must not be shared across performances. In other words, they should be scoped to only a single performance.
    /// </para>
    /// <para>
    /// A single performance might involve only one actor or it might involve many.
    /// If a performance makes use of multiple actors, the developer should consider making use of either or both of the <see cref="ICast"/>
    /// and/or <see cref="IStage"/>, which assist in the management of multiple actors.
    /// </para>
    /// </remarks>
    /// <example>
    /// <para>
    /// A good example of an actor, aside from a person, could be task scheduling software which executes logic at specific times of
    /// day.  That is - provided that the task scheduling software itself is not a part of the Screenplay Performance.
    /// Within the context of such a performance, the task scheduler executing its logic at the configured time would appear to be an
    /// autonomous action because nothing else within the scope of the performance prompted it.
    /// </para>
    /// <para>
    /// Conversely, a mouse trap would be a poor example of an actor; a mouse trap acts only reactively. The creature entering the trap
    /// would be the actor in such a performance.
    /// </para>
    /// </example>
    /// <seealso cref="IPerformance"/>
    /// <seealso cref="ICast"/>
    /// <seealso cref="IStage"/>
    public partial class Actor : IHasName, IHasPerformanceIdentity
    {
        const string unnamedActor = "<Unnamed actor>";

        readonly string name;
        readonly Guid performanceIdentity;

        /// <summary>Gets the actor's name</summary>
        /// <seealso cref="IHasName"/>
        public virtual string Name => name;

        /// <summary>Gets the unique identity for the performance in which this actor is participating</summary>
        protected virtual Guid PerformanceIdentity => performanceIdentity;

        Guid IHasPerformanceIdentity.PerformanceIdentity => PerformanceIdentity;

        /// <summary>Initialises a new instance of <see cref="Actor"/></summary>
        /// <remarks>
        /// <para>
        /// In line with <see cref="IHasName"/>, it is strongly recommended that <paramref name="name"/> is not <see langword="null"/>.
        /// </para>
        /// </remarks>
        /// <param name="name">The actor's name</param>
        /// <param name="performanceIdentity">A unique identity for the performance</param>
        public Actor(string name, Guid performanceIdentity)
        {
            this.name = name ?? unnamedActor;
            this.performanceIdentity = performanceIdentity;
        }
    }
}