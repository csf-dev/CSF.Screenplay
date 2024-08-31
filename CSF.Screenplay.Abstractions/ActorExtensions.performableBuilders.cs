using System;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;

namespace CSF.Screenplay
{
    public static partial class ActorExtensions
    {
        /// <summary>
        /// Performs an action or task which returns no result from the performable which is exposed by the specified builder object.
        /// </summary>
        /// <param name="actor">An actor</param>
        /// <param name="performableBuilder">The performable builder</param>
        /// <param name="cancellationToken">An optional token to cancel the performable</param>
        /// <returns>A task which completes when the performable is complete</returns>
        public static ValueTask PerformAsync(this ICanPerform actor, IGetsPerformable performableBuilder, CancellationToken cancellationToken = default)
        {
            if (actor is null)
                throw new ArgumentNullException(nameof(actor));
            if (performableBuilder is null)
                throw new ArgumentNullException(nameof(performableBuilder));
            return actor.PerformAsync(performableBuilder.GetPerformable(), cancellationToken);
        }

        /// <summary>
        /// Performs an action or task which returns an untyped result from the performable which is exposed by the specified builder object.
        /// </summary>
        /// <param name="actor">An actor</param>
        /// <param name="performableBuilder">The performable builder</param>
        /// <param name="cancellationToken">An optional token to cancel the performable</param>
        /// <returns>A task which exposes a result when the performable is complete</returns>
        public static ValueTask<object> PerformAsync(this ICanPerform actor, IGetsPerformableWithResult performableBuilder, CancellationToken cancellationToken = default)
        {
            if (actor is null)
                throw new ArgumentNullException(nameof(actor));
            if (performableBuilder is null)
                throw new ArgumentNullException(nameof(performableBuilder));
            return actor.PerformAsync(performableBuilder.GetPerformable(), cancellationToken);
        }

        /// <summary>
        /// Performs an action or task which returns a strongly typed result from the performable which is exposed by the specified builder object.
        /// </summary>
        /// <param name="actor">An actor</param>
        /// <param name="performableBuilder">The performable builder</param>
        /// <param name="cancellationToken">An optional token to cancel the performable</param>
        /// <typeparam name="T">The result type</typeparam>
        /// <returns>A task which exposes a result when the performable is complete</returns>
        public static ValueTask<T> PerformAsync<T>(this ICanPerform actor, IGetsPerformableWithResult<T> performableBuilder, CancellationToken cancellationToken = default)
        {
            if (actor is null)
                throw new ArgumentNullException(nameof(actor));
            if (performableBuilder is null)
                throw new ArgumentNullException(nameof(performableBuilder));
            return actor.PerformAsync(performableBuilder.GetPerformable(), cancellationToken);
        }

        /// <summary>
        /// Performs an action or task which returns no result.
        /// </summary>
        /// <param name="actor">An actor</param>
        /// <param name="performableBuilder">The performable builder</param>
        /// <param name="cancellationToken">An optional token to cancel the performable</param>
        /// <returns>A task which completes when the performable is complete</returns>
        public static ValueTask WasAbleTo(this ICanPerformGiven actor, IGetsPerformable performableBuilder, CancellationToken cancellationToken = default)
        {
            if (actor is null)
                throw new ArgumentNullException(nameof(actor));
            if (performableBuilder is null)
                throw new ArgumentNullException(nameof(performableBuilder));
            return actor.WasAbleTo(performableBuilder.GetPerformable(), cancellationToken);
        }

        /// <summary>
        /// Performs an action or task which returns an untyped result.
        /// </summary>
        /// <param name="actor">An actor</param>
        /// <param name="performableBuilder">The performable builder</param>
        /// <param name="cancellationToken">An optional token to cancel the performable</param>
        /// <returns>A task which exposes a result when the performable is complete</returns>
        public static ValueTask<object> WasAbleTo(this ICanPerformGiven actor, IGetsPerformableWithResult performableBuilder, CancellationToken cancellationToken = default)
        {
            if (actor is null)
                throw new ArgumentNullException(nameof(actor));
            if (performableBuilder is null)
                throw new ArgumentNullException(nameof(performableBuilder));
            return actor.WasAbleTo(performableBuilder.GetPerformable(), cancellationToken);
        }

        /// <summary>
        /// Performs an action or task which returns a strongly typed result.
        /// </summary>
        /// <param name="actor">An actor</param>
        /// <param name="performableBuilder">The performable builder</param>
        /// <param name="cancellationToken">An optional token to cancel the performable</param>
        /// <typeparam name="T">The result type</typeparam>
        /// <returns>A task which exposes a result when the performable is complete</returns>
        public static ValueTask<T> WasAbleTo<T>(this ICanPerformGiven actor, IGetsPerformableWithResult<T> performableBuilder, CancellationToken cancellationToken = default)
        {
            if (actor is null)
                throw new ArgumentNullException(nameof(actor));
            if (performableBuilder is null)
                throw new ArgumentNullException(nameof(performableBuilder));
            return actor.WasAbleTo(performableBuilder.GetPerformable(), cancellationToken);
        }

        /// <summary>
        /// Performs an action or task which returns no result.
        /// </summary>
        /// <param name="actor">An actor</param>
        /// <param name="performableBuilder">The performable builder</param>
        /// <param name="cancellationToken">An optional token to cancel the performable</param>
        /// <returns>A task which completes when the performable is complete</returns>
        public static ValueTask AttemptsTo(this ICanPerformWhen actor, IGetsPerformable performableBuilder, CancellationToken cancellationToken = default)
        {
            if (actor is null)
                throw new ArgumentNullException(nameof(actor));
            if (performableBuilder is null)
                throw new ArgumentNullException(nameof(performableBuilder));
            return actor.AttemptsTo(performableBuilder.GetPerformable(), cancellationToken);
        }

        /// <summary>
        /// Performs an action or task which returns an untyped result.
        /// </summary>
        /// <param name="actor">An actor</param>
        /// <param name="performableBuilder">The performable builder</param>
        /// <param name="cancellationToken">An optional token to cancel the performable</param>
        /// <returns>A task which exposes a result when the performable is complete</returns>
        public static ValueTask<object> AttemptsTo(this ICanPerformWhen actor, IGetsPerformableWithResult performableBuilder, CancellationToken cancellationToken = default)
        {
            if (actor is null)
                throw new ArgumentNullException(nameof(actor));
            if (performableBuilder is null)
                throw new ArgumentNullException(nameof(performableBuilder));
            return actor.AttemptsTo(performableBuilder.GetPerformable(), cancellationToken);
        }

        /// <summary>
        /// Performs an action or task which returns a strongly typed result.
        /// </summary>
        /// <param name="actor">An actor</param>
        /// <param name="performableBuilder">The performable builder</param>
        /// <param name="cancellationToken">An optional token to cancel the performable</param>
        /// <typeparam name="T">The result type</typeparam>
        /// <returns>A task which exposes a result when the performable is complete</returns>
        public static ValueTask<T> AttemptsTo<T>(this ICanPerformWhen actor, IGetsPerformableWithResult<T> performableBuilder, CancellationToken cancellationToken = default)
        {
            if (actor is null)
                throw new ArgumentNullException(nameof(actor));
            if (performableBuilder is null)
                throw new ArgumentNullException(nameof(performableBuilder));
            return actor.AttemptsTo(performableBuilder.GetPerformable(), cancellationToken);
        }

        /// <summary>
        /// Performs an action or task which returns no result.
        /// </summary>
        /// <param name="actor">An actor</param>
        /// <param name="performableBuilder">The performable builder</param>
        /// <param name="cancellationToken">An optional token to cancel the performable</param>
        /// <returns>A task which completes when the performable is complete</returns>
        public static ValueTask Should(this ICanPerformThen actor, IGetsPerformable performableBuilder, CancellationToken cancellationToken = default)
        {
            if (actor is null)
                throw new ArgumentNullException(nameof(actor));
            if (performableBuilder is null)
                throw new ArgumentNullException(nameof(performableBuilder));
            return actor.Should(performableBuilder.GetPerformable(), cancellationToken);
        }

        /// <summary>
        /// Performs an action or task which returns an untyped result.
        /// </summary>
        /// <param name="actor">An actor</param>
        /// <param name="performableBuilder">The performable builder</param>
        /// <param name="cancellationToken">An optional token to cancel the performable</param>
        /// <returns>A task which exposes a result when the performable is complete</returns>
        public static ValueTask<object> Should(this ICanPerformThen actor, IGetsPerformableWithResult performableBuilder, CancellationToken cancellationToken = default)
        {
            if (actor is null)
                throw new ArgumentNullException(nameof(actor));
            if (performableBuilder is null)
                throw new ArgumentNullException(nameof(performableBuilder));
            return actor.Should(performableBuilder.GetPerformable(), cancellationToken);
        }

        /// <summary>
        /// Performs an action or task which returns a strongly typed result.
        /// </summary>
        /// <param name="actor">An actor</param>
        /// <param name="performableBuilder">The performable builder</param>
        /// <param name="cancellationToken">An optional token to cancel the performable</param>
        /// <typeparam name="T">The result type</typeparam>
        /// <returns>A task which exposes a result when the performable is complete</returns>
        public static ValueTask<T> Should<T>(this ICanPerformThen actor, IGetsPerformableWithResult<T> performableBuilder, CancellationToken cancellationToken = default)
        {
            if (actor is null)
                throw new ArgumentNullException(nameof(actor));
            if (performableBuilder is null)
                throw new ArgumentNullException(nameof(performableBuilder));
            return actor.Should(performableBuilder.GetPerformable(), cancellationToken);
        }
    }
}