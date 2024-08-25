using System;

namespace CSF.Screenplay
{
    /// <summary>A persona is a factory for a commonly-used actor</summary>
    /// <remarks>
    /// <para>
    /// In Screenplay is is recommended to use memorable actors which are widely understood and recognisable
    /// by the team. This is easier if the composition of an actor is the same across every <see cref="Performance"/> in which
    /// they participate.
    /// </para>
    /// <para>
    /// By using a separate persona implementation for each named actor, the developer can ensure consistent creation
    /// for instances of those actors.
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