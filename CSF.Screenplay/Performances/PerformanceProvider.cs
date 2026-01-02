using System;

namespace CSF.Screenplay.Performances
{
    /// <summary>
    /// A class which provides access to the current <see cref="IPerformance"/>, which exists within the
    /// current dependency injection scope.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This container/provider class is required for architectural reasons.  Instances of <see cref="Performance"/> cannot be created upon their
    /// first resolution from dependency injection.  This is because performances are immutable and require constructor parameters which are provided
    /// by the creating logic.  However, performance must also be available instance-per-scope from DI.  That is why this class is required, so that
    /// a created performance may be 'registered' with this class, with <see cref="SetCurrentPerformance"/>.
    /// This class is itself registered in DI as instance-per-scope.  So, consuming logic may resolve a performance by getting an instance of this
    /// provider and then using <see cref="GetCurrentPerformance"/>.
    /// </para>
    /// </remarks>
    public class PerformanceProvider
    {
        IPerformance performance;

        /// <summary>
        /// Gets the current performance from this provider instance.
        /// </summary>
        /// <returns>The current performance</returns>
        /// <exception cref="InvalidOperationException">If this instance does not have a current performance.</exception>
        public IPerformance GetCurrentPerformance()
            => performance ?? throw new InvalidOperationException($"There must be a current performance; use {nameof(SetCurrentPerformance)} first");

        /// <summary>
        /// Sets the current performance for this provider instance.
        /// </summary>
        /// <param name="currentPerformance">The new current performance</param>
        /// <exception cref="ArgumentNullException">If <paramref name="currentPerformance"/> is <see langword="null" /></exception>
        /// <exception cref="InvalidOperationException">If the current instance already has a current performance.</exception>
        public void SetCurrentPerformance(IPerformance currentPerformance)
        {
            if(currentPerformance == null) throw new ArgumentNullException(nameof(currentPerformance));
            if(performance != null)
                throw new InvalidOperationException($"An instance of {nameof(PerformanceProvider)} may only have its current performance set " +
                                                    "once. Performance providers are not re-usable.");
            performance = currentPerformance;
        }
    }
}