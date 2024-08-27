using System;
using System.Collections.Generic;

namespace CSF.Screenplay
{
    /// <summary>An object which has &amp; is able to gain abilities.</summary>
    /// <remarks>
    /// <para>
    /// Abilities are the mechanism by which actors: <see cref="ICanPerform"/> interact with the application and system.
    /// They are arbitrary objects which provide functionality.
    /// </para>
    /// </remarks>
    public interface IHasAbilities
    {
        /// <summary>Gets the collection of the actor's abilities.</summary>
        IReadOnlyCollection<object> Abilities { get; }

        /// <summary>Adds an ability to the specified actor</summary>
        /// <param name="ability">The ability to add to the actor</param>
        /// <exception cref="ArgumentNullException">If <paramref name="ability"/> is <see langword="null" /></exception>
        /// <exception cref="InvalidOperationException">If the actor already has an ability of the same type as
        /// <paramref name="ability"/>, or which derives from the same type</exception>
        void IsAbleTo(object ability);
    }
}