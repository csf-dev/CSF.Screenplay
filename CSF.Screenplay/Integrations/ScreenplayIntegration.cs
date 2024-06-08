using System;
using CSF.Screenplay.Performances;

namespace CSF.Screenplay.Integrations
{
    /// <summary>The default implementation of <see cref="IScreenplay"/> which provides the central point of integration.</summary>
    public class ScreenplayIntegration : IScreenplay, IHasScreenplayEvents
    {
        readonly ICreatesPerformance performanceFactory;

        /// <inheritdoc/>
        public virtual ICreatesPerformance PerformanceFactory => performanceFactory;
        
        /// <inheritdoc/>
        public event EventHandler ScreenplayBegun;

        /// <inheritdoc/>
        public event EventHandler ScreenplayCompleted;

        /// <inheritdoc/>
        public virtual void BeginScreenplay() => ScreenplayBegun?.Invoke(this, EventArgs.Empty);

        /// <inheritdoc/>
        public virtual void CompleteScreenplay() => ScreenplayCompleted?.Invoke(this, EventArgs.Empty);

        /// <inheritdoc/>
        public virtual void StartPerformance(Performance performance)
        {
            if (performance is null) throw new ArgumentNullException(nameof(performance));
            performance.BeginPerformance();
        }

        /// <inheritdoc/>
        public virtual void CompletePerformance(Performance performance, bool? success)
        {
            if (performance is null) throw new ArgumentNullException(nameof(performance));
            performance.CompletePerformance(success);
        }

        /// <summary>Initialises a new instance of <see cref="ScreenplayIntegration"/></summary>
        /// <param name="performanceFactory">The factory for performance instances</param>
        /// <exception cref="ArgumentNullException">If the performance factory is <see langword="null" /></exception>
        public ScreenplayIntegration(ICreatesPerformance performanceFactory)
        {
            this.performanceFactory = performanceFactory ?? throw new ArgumentNullException(nameof(performanceFactory));
        }
    }
}