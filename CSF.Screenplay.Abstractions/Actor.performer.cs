using System;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay
{
    public partial class Actor : ICanPerform
    {
        /// <summary>
        /// Performs an action or task which returns no result.
        /// </summary>
        /// <param name="performable">The performable item</param>
        /// <param name="cancellationToken">An optional token to cancel the performable</param>
        /// <param name="phase">The performance phase to which the performable belongs</param>
        /// <returns>A task which completes when the performable is complete</returns>
        /// <exception cref="ArgumentNullException">If the performable is <see langword="null" /></exception>
        protected virtual async ValueTask PerformAsync(IPerformable performable,
                                                  CancellationToken cancellationToken = default,
                                                  PerformancePhase phase = PerformancePhase.Unspecified)
        {
            if(performable is null) throw new ArgumentNullException(nameof(performable));
            AssertNotDisposed();

            try
            {
                InvokeBeginPerformable(performable, phase);
                await performable.PerformAsAsync(this, cancellationToken).ConfigureAwait(false);    
                InvokeEndPerformable(performable, phase);
            }
            catch(Exception ex)
            {
                InvokePerformableFailed(performable, ex, phase);
                throw;
            }
        }

        ValueTask ICanPerform.PerformAsync(IPerformable performable, CancellationToken cancellationToken)
            => PerformAsync(performable, cancellationToken);

        /// <summary>
        /// Performs an action or task which returns an untyped result.
        /// </summary>
        /// <param name="performable">The performable item</param>
        /// <param name="cancellationToken">An optional token to cancel the performable</param>
        /// <param name="phase">The performance phase to which the performable belongs</param>
        /// <returns>A task which completes when the performable is complete</returns>
        /// <exception cref="ArgumentNullException">If the performable is <see langword="null" /></exception>
        protected virtual async ValueTask<object> PerformAsync(IPerformableWithResult performable,
                                                          CancellationToken cancellationToken = default,
                                                          PerformancePhase phase = PerformancePhase.Unspecified)
        {
            if(performable is null) throw new ArgumentNullException(nameof(performable));
            AssertNotDisposed();

            try
            {
                InvokeBeginPerformable(performable, phase);
                var result = await performable.PerformAsAsync(this, cancellationToken).ConfigureAwait(false);    
                InvokePerformableResult(performable, result);
                InvokeEndPerformable(performable, phase);
                return result;
            }
            catch(Exception ex)
            {
                InvokePerformableFailed(performable, ex, phase);
                throw;
            }
        }

        ValueTask<object> ICanPerform.PerformAsync(IPerformableWithResult performable, CancellationToken cancellationToken)
            => PerformAsync(performable, cancellationToken);

        /// <summary>
        /// Performs an action or task which returns a strongly typed result.
        /// </summary>
        /// <param name="performable">The performable item</param>
        /// <param name="cancellationToken">An optional token to cancel the performable</param>
        /// <param name="phase">The performance phase to which the performable belongs</param>
        /// <returns>A task which completes when the performable is complete</returns>
        /// <exception cref="ArgumentNullException">If the performable is <see langword="null" /></exception>
        protected virtual async ValueTask<T> PerformAsync<T>(IPerformableWithResult<T> performable,
                                                        CancellationToken cancellationToken = default,
                                                        PerformancePhase phase = PerformancePhase.Unspecified)
        {
            if(performable is null) throw new ArgumentNullException(nameof(performable));
            AssertNotDisposed();

            try
            {
                InvokeBeginPerformable(performable, phase);
                var result = await performable.PerformAsAsync(this, cancellationToken).ConfigureAwait(false);    
                InvokePerformableResult(performable, result);
                InvokeEndPerformable(performable, phase);
                return result;
            }
            catch(Exception ex)
            {
                InvokePerformableFailed(performable, ex, phase);
                throw;
            }
        }

        ValueTask<T> ICanPerform.PerformAsync<T>(IPerformableWithResult<T> performable, CancellationToken cancellationToken)
            => PerformAsync(performable, cancellationToken);
    }
}