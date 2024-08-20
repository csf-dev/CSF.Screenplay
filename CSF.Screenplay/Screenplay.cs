using System;
using CSF.Screenplay.Performances;

namespace CSF.Screenplay
{
    /// <summary>An object which represents a complete execution of Screenplay logic, which should include one or
    /// more <see cref="Performance"/> instances.</summary>
    /// <remarks>
    /// <para>
    /// A Screenplay, when used as a noun (an instance of this class), refers to a complete execution of the Screenplay
    /// software.
    /// A Screenplay is composed of at least one <see cref="Performance"/> and typically contains many performances.
    /// </para>
    /// <para>
    /// When the Screenplay architecture is applied to automated testing, an instance of this class corresponds to a
    /// complete test run, where each test corresponds to a performance.
    /// End-user logic, such as test logic, rarely interacts directly with this class.
    /// That is because the Screenplay object is generally consumed only by <xref href="IntegrationGlossaryItem?text=integration+logic"/>.
    /// </para>
    /// <para>
    /// The Screenplay object is used to create instances of <see cref="Performance"/> via the <see cref="PerformanceFactory"/>.
    /// You may wish to read a <xref href="HowScreenplayAndPerformanceRelateArticle?text=diagram+showing+how+screenplays,+performances,+actors+and+performables+relate+to+one+another" />.
    /// </para>
    /// </remarks>
    /// <seealso cref="Performance"/>
    public sealed class Screenplay
    {
        readonly ICreatesPerformance performanceFactory;

        /// <summary>Gets the factory which should be used to create new instances of <see cref="Performance"/> within the
        /// current Screenplay.</summary>
        public ICreatesPerformance PerformanceFactory => performanceFactory;
        
        /// <summary>Occurs at the beginning of a Screenplay process.</summary>
        public event EventHandler ScreenplayBegun;

        /// <summary>Occurs when the Screenplay process has completed.</summary>
        public event EventHandler ScreenplayCompleted;

        /// <summary>Execute this method from the consuming logic in order to inform the Screenplay architecture that the
        /// Screenplay has begun.</summary>
        public void BeginScreenplay() => ScreenplayBegun?.Invoke(this, EventArgs.Empty);

        /// <summary>Execute this method from the consuming logic in order to inform the Screenplay architecture that the
        /// Screenplay is now complete.</summary>
        public void CompleteScreenplay() => ScreenplayCompleted?.Invoke(this, EventArgs.Empty);

        /// <summary>Execute this method from the consuming logic in order to inform the Screenplay architecture the specified
        /// performance has begun.</summary>
        /// <param name="performance">The performance which has begun</param>
        public void StartPerformance(Performance performance)
        {
            if (performance is null) throw new ArgumentNullException(nameof(performance));
            performance.BeginPerformance();
        }

        /// <summary>Execute this method from the consuming logic in order to inform the Screenplay architecture the specified
        /// performance has has completed, and that its result should be recorded.</summary>
        /// <param name="performance">The performance which has completed</param>
        /// <param name="success">A value indicating the outcome of the performance; see
        /// <see cref="Performance.CompletePerformance(bool?)"/> for more information</param>
        public void CompletePerformance(Performance performance, bool? success)
        {
            if (performance is null) throw new ArgumentNullException(nameof(performance));
            performance.CompletePerformance(success);
        }

        /// <summary>Initialises a new instance of <see cref="Screenplay"/></summary>
        /// <param name="performanceFactory">The factory for performance instances</param>
        /// <exception cref="ArgumentNullException">If the performance factory is <see langword="null" /></exception>
        public Screenplay(ICreatesPerformance performanceFactory)
        {
            this.performanceFactory = performanceFactory ?? throw new ArgumentNullException(nameof(performanceFactory));
        }
    }
}