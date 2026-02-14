using System;

namespace CSF.Screenplay
{
    /// <summary>A persona is a factory for a commonly-used <see cref="Actor"/></summary>
    /// <remarks>
    /// <para>
    /// In Screenplay is is recommended to use &amp; reuse memorable actors, which are widely understood and recognisable
    /// to the development team. This is easier if the composition of an actor is the same across every
    /// <see cref="IPerformance"/> in which they participate. Personas facilitate this; the persona class serves
    /// as a consistent factory which creates the same named actor in the same manner every time.
    /// </para>
    /// <para>
    /// Developers should create an implementation of this interface for each actor which they wish to
    /// define, ideally named the same as the name as the Actor.  Each persona implementation should
    /// name the actor and grant them <xref href="AbilityGlossaryItem?text=the+abilities"/> which are appropriate.
    /// </para>
    /// <para>
    /// Instance of persona classes are resolved from the Dependency Injection container, so it is appropriate
    /// and correct to constructor-inject any services which are required in order to get the ability instances.
    /// </para>
    /// </remarks>
    public interface IPersona : IHasName
    {
        /// <summary>Gets the actor which is associated with the current persona</summary>
        /// <remarks>
        /// <para>
        /// Implementors should not only create and return the actor from this method, but also
        /// configure the actor with the standard abilities associated with this persona.
        /// </para>
        /// </remarks>
        /// <param name="performanceIdentity">A unique identity for the currently-executing performance</param>
        Actor GetActor(Guid performanceIdentity);
    }
}
