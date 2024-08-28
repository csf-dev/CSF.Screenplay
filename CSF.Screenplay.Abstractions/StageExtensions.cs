using System;

namespace CSF.Screenplay
{
    /// <summary>
    /// Extension methods for <see cref="IStage"/>.
    /// </summary>
    public static class StageExtensions
    {
        /// <summary>Places an actor matching the specified persona into the spotlight, making them 'the current actor' on this stage.</summary>
        /// <remarks>
        /// <para>
        /// A maximum of one actor may be in the spotlight at any time, so if a different actor is already in the spotlight as this
        /// method is used, then they will be implicitly removed and replaced by the actor derived from the persona.
        /// The actor who is in the spotlight may be retrieved by calling <see cref="IStage.GetSpotlitActor"/>.
        /// </para>
        /// <para>
        /// If actor indicated by the persona is already in the spotlight then this method will have no effect, the actor will remain
        /// in the spotlight.
        /// </para>
        /// <para>
        /// When spotlighting a persona, the actor instance is retrieved from an <see cref="ICast"/> based upon that same persona.
        /// See <see cref="ICast.GetActor(IPersona)"/> for more information.
        /// </para>
        /// <para>To remove an actor from the spotlight without replacing them, use <see cref="IStage.TurnSpotlightOff"/>.</para>
        /// <para>
        /// This method is the recommended way of putting an actor based upon a persona in the spotlight, as it takes care of
        /// resolving the persona from dependency injection automatically.
        /// </para>
        /// </remarks>
        /// <returns>The actor instance which was placed into the spotlight.</returns>
        /// <param name="stage">The stage on which to spotlight the actor.</param>
        /// <exception cref="ArgumentNullException">If the actor is <see langword="null" />.</exception>
        /// <typeparam name="TPersona">A concrete type of <see cref="IPersona"/> from which to get an actor.</typeparam>
        public static Actor Spotlight<TPersona>(this IStage stage) where TPersona : class,IPersona
        {
            if (stage is null)
                throw new ArgumentNullException(nameof(stage));

            var actor = stage.Cast.GetActor<TPersona>();
            stage.Spotlight(actor);
            return actor;
        }

    }
}