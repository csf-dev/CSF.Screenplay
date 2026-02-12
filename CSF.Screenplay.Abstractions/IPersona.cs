using System;

namespace CSF.Screenplay
{
    /// <summary>A persona is a factory for a commonly-used actor</summary>
    /// <remarks>
    /// <para>
    /// In Screenplay is is recommended to use &amp; reuse memorable actors, which are widely understood and recognisable
    /// to the development team. This is easier if the composition of an actor is the same across every <see cref="IPerformance"/> in which
    /// they participate.
    /// </para>
    /// <para>
    /// Use classes which derive from this interface to define those memorable
    /// actors. The persona class should configure the actor it creates with a name and
    /// a set of abilities appropriate to the actor.
    /// </para>
    /// <para>
    /// Classes which derive from this interface should constructor-inject any dependencies
    /// they require, such as those required to grant abilities.
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
