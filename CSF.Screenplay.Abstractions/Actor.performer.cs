using System;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;

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
                                     PerformancePhase phase = PerformancePhase.Unspecified,
                                     CancellationToken cancellationToken = default)
        {
            if(performable is null) throw new ArgumentNullException(nameof(performable));
            AssertNotDisposed();

            try
            {
                InvokeBeginPerformable(performable, phase);
                await performable.PerformAsAsync(this, cancellationToken).ConfigureAwait(false);    
                InvokeEndPerformable(performable, phase);
            }
            catch(PerformableException ex)
            {
                InvokePerformableFailed(performable, ex, phase);
                throw;
            }
            catch(Exception ex)
            {
                InvokePerformableFailed(performable, ex, phase);
                throw GetPerformableException(performable, ex);
            }
        }

        /// <summary>
        /// Performs a question or question-like task which returns an untyped result.
        /// </summary>
        /// <param name="performable">The performable item</param>
        /// <param name="cancellationToken">An optional token to cancel the performable</param>
        /// <param name="phase">The performance phase to which the performable belongs</param>
        /// <returns>A task which completes when the performable is complete</returns>
        /// <exception cref="ArgumentNullException">If the performable is <see langword="null" /></exception>
        protected virtual ValueTask<object> PerformAsync(IPerformableWithResult performable,
                                                         PerformancePhase phase = PerformancePhase.Unspecified,
                                                         CancellationToken cancellationToken = default)
        {
            if(performable is null) throw new ArgumentNullException(nameof(performable));
            AssertNotDisposed();

            return PerformAsync(performable, phase, () => performable.PerformAsAsync(this, cancellationToken));
        }

        /// <summary>
        /// Performs a question or question-like task which returns a strongly typed result.
        /// </summary>
        /// <param name="performable">The performable item</param>
        /// <param name="cancellationToken">An optional token to cancel the performable</param>
        /// <param name="phase">The performance phase to which the performable belongs</param>
        /// <returns>A task which completes when the performable is complete</returns>
        /// <exception cref="ArgumentNullException">If the performable is <see langword="null" /></exception>
        protected virtual ValueTask<T> PerformAsync<T>(IPerformableWithResult<T> performable,
                                                       PerformancePhase phase = PerformancePhase.Unspecified,
                                                       CancellationToken cancellationToken = default)
        {
            if(performable is null) throw new ArgumentNullException(nameof(performable));
            AssertNotDisposed();

            return PerformAsync(performable, phase, () => performable.PerformAsAsync(this, cancellationToken));
        }

        PerformableException GetPerformableException(object performable, Exception ex)
        {
            return new PerformableException($"{Name} encountered an unexpected exception whilst performing {DefaultStrings.FormatValue(performable)}", ex)
            {
                Performable = performable,
            };
        }

        async ValueTask<T> PerformAsync<T>(object performable, PerformancePhase phase, Func<ValueTask<T>> performableFunc)
        {
            try
            {
                InvokeBeginPerformable(performable, phase);
                var result = await performableFunc().ConfigureAwait(false);    
                InvokePerformableResult(performable, result);
                InvokeEndPerformable(performable, phase);
                return result;
            }
            catch(PerformableException ex)
            {
                InvokePerformableFailed(performable, ex, phase);
                throw;
            }
            catch(Exception ex)
            {
                InvokePerformableFailed(performable, ex, phase);
                throw GetPerformableException(performable, ex);
            }
        }

        ValueTask ICanPerform.PerformAsync(IPerformable performable, CancellationToken cancellationToken)
            => PerformAsync(performable, cancellationToken: cancellationToken);

        ValueTask<object> ICanPerform.PerformAsync(IPerformableWithResult performable, CancellationToken cancellationToken)
            => PerformAsync(performable, cancellationToken: cancellationToken);

        ValueTask<T> ICanPerform.PerformAsync<T>(IPerformableWithResult<T> performable, CancellationToken cancellationToken)
            => PerformAsync(performable, cancellationToken: cancellationToken);

        void ICanPerform.RecordAsset(object performable, string filePath, string fileSummary)
            => InvokeRecordsAsset(performable, filePath, fileSummary);
    }
}