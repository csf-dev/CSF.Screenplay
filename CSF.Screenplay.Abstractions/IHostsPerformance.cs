using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Performances;

namespace CSF.Screenplay
{
    /// <summary>
    /// An object which encapsulates the logic of an <see cref="IPerformance"/> in a standalone Screenplay.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Implementors should inject any dependencies they require into their constructors.
    /// The <see cref="ExecutePerformanceAsync(CancellationToken)"/> method is used to execute the logic of
    /// an <see cref="IPerformance"/>, returning its result.
    /// </para>
    /// </remarks>
    public interface IHostsPerformance
    {
        /// <summary>
        /// Executes the logic of a performance, returning the result.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The result of the performance has the same semantics as <see cref="IBeginsAndEndsPerformance.FinishPerformance(bool?)"/>.
        /// Implementors should use this method to execute the logic of the performance.
        /// </para>
        /// </remarks>
        /// <param name="cancellationToken">A cancellation token</param>
        /// <returns>A task which exposes the result of the performance.</returns>
        Task<bool?> ExecutePerformanceAsync(CancellationToken cancellationToken);
    }
}

