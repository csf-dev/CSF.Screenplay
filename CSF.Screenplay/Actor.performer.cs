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
        /// <param name="cancellationToken">An optional token to cancel the performance</param>
        /// <param name="phase">The phase to which the performance belongs</param>
        /// <returns>A task which completes when the performance is complete</returns>
        /// <exception cref="ArgumentNullException">If the performable is <see langword="null" /></exception>
        protected virtual async Task PerformAsync(IPerformable performable,
                                                  CancellationToken cancellationToken = default,
                                                  PerformancePhase phase = PerformancePhase.Unspecified)
        {
            if(performable is null) throw new ArgumentNullException(nameof(performable));

            try
            {
                InvokeBeginPerformance(performable, phase);
                await performable.PerformAsAsync(this, cancellationToken).ConfigureAwait(false);    
                InvokeEndPerformance(performable, phase);
            }
            catch(Exception ex)
            {
                InvokePerformanceFailed(performable, ex, phase);
                throw;
            }
        }

        Task ICanPerform.PerformAsync(IPerformable performable, CancellationToken cancellationToken)
            => PerformAsync(performable, cancellationToken);

        /// <summary>
        /// Performs an action or task which returns an untyped result.
        /// </summary>
        /// <param name="performable">The performable item</param>
        /// <param name="cancellationToken">An optional token to cancel the performance</param>
        /// <param name="phase">The phase to which the performance belongs</param>
        /// <returns>A task which completes when the performance is complete</returns>
        /// <exception cref="ArgumentNullException">If the performable is <see langword="null" /></exception>
        protected virtual async Task<object> PerformAsync(IPerformableWithResult performable,
                                                          CancellationToken cancellationToken = default,
                                                          PerformancePhase phase = PerformancePhase.Unspecified)
        {
            if(performable is null) throw new ArgumentNullException(nameof(performable));

            try
            {
                InvokeBeginPerformance(performable, phase);
                var result = await performable.PerformAsAsync(this, cancellationToken).ConfigureAwait(false);    
                InvokePerformanceResult(performable, result);
                InvokeEndPerformance(performable, phase);
                return result;
            }
            catch(Exception ex)
            {
                InvokePerformanceFailed(performable, ex, phase);
                throw;
            }
        }

        Task<object> ICanPerform.PerformAsync(IPerformableWithResult performable, CancellationToken cancellationToken)
            => PerformAsync(performable, cancellationToken);

        /// <summary>
        /// Performs an action or task which returns a strongly typed result.
        /// </summary>
        /// <param name="performable">The performable item</param>
        /// <param name="cancellationToken">An optional token to cancel the performance</param>
        /// <param name="phase">The phase to which the performance belongs</param>
        /// <returns>A task which completes when the performance is complete</returns>
        /// <exception cref="ArgumentNullException">If the performable is <see langword="null" /></exception>
        protected virtual async Task<T> PerformAsync<T>(IPerformableWithResult<T> performable,
                                                        CancellationToken cancellationToken = default,
                                                        PerformancePhase phase = PerformancePhase.Unspecified)
        {
            if(performable is null) throw new ArgumentNullException(nameof(performable));

            try
            {
                InvokeBeginPerformance(performable, phase);
                var result = await performable.PerformAsAsync(this, cancellationToken).ConfigureAwait(false);    
                InvokePerformanceResult(performable, result);
                InvokeEndPerformance(performable, phase);
                return result;
            }
            catch(Exception ex)
            {
                InvokePerformanceFailed(performable, ex, phase);
                throw;
            }
        }

        Task<T> ICanPerform.PerformAsync<T>(IPerformableWithResult<T> performable, CancellationToken cancellationToken)
            => PerformAsync(performable, cancellationToken);
    }
}