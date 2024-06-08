using System;
using System.Collections.Generic;
using System.Linq;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performances;

namespace CSF.Screenplay
{
    /// <summary>A performance represents a self-contained scope of performables which typically results in overall success or failure.</summary>
    /// <remarks>
    /// <para>
    /// The performance is a scope of Screenplay logic.  In testing frameworks this might be called a "scenario", or a "test", or "test case", or "theory".
    /// It is a series of actions/performables with a beginning, middle and end. Actors participate in the performance and drive the performables forward.
    /// Typically a performance will complete in either success or failure.
    /// </para>
    /// <para>
    /// This performance object represents the scope or lifetime of that performance.
    /// Associated with it is a dependency injection lifetime scope.
    /// </para>
    /// </remarks>
    public sealed class Performance : IHasPerformanceIdentity,
                                      IHasServiceProvider,
                                      IEquatable<Performance>,
                                      IDisposable,
                                      IHasScenarioHierarchy,
                                      IHasScenarioEvents,
                                      IBeginsAndEndsPerformance
    {
        bool hasBegun, hasCompleted;
        bool? success;

        /// <inheritdoc/>
        public Guid PerformanceIdentity { get; }

        /// <inheritdoc/>
        public IServiceProvider ServiceProvider { get; }

        /// <summary>Gets an ordered list which indicates the current object's position within the scenario hierarchy.</summary>
        /// <remarks>
        /// <para>
        /// Identifiers and names which are earlier in this list are considered to be 'parents' within the hierarchy. Items subsequent
        /// in this list are hierarchical descendents of the preceding list items.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// If you wished to indicate that the current object is a scenario named <c>Joe can take out the Trash</c>, which is part of a
        /// feature named <c>Joe can do his chores</c> then the first item in this list should be <c>Joe can do his chores</c> (the parent
        /// feature) and the second item <c>Joe can take out the Trash</c> (the scenario which is a child of that feature).
        /// </para>
        /// </example>
        public List<IdentifierAndName> ScenarioHierarchy { get; } = new List<IdentifierAndName>();

        IReadOnlyList<IdentifierAndName> IHasScenarioHierarchy.ScenarioHierarchy => ScenarioHierarchy;

        /// <summary>Gets a value which indicates the state of the current performance.</summary>
        public ScenarioState ScenarioState
        {
            get {
                if (!hasBegun) return ScenarioState.NotStarted;
                if (!hasCompleted) return ScenarioState.InProgress;
                switch(success)
                {
                    case true: return ScenarioState.Success;
                    case false: return ScenarioState.Failed;
                    default: return ScenarioState.Completed;
                }
            }
        }

        /// <inheritdoc/>
        public event EventHandler<ScenarioEventArgs> Begin;

        /// <inheritdoc/>
        public void BeginPerformance()
        {
            if(hasBegun) throw new InvalidOperationException($"An instance of {nameof(Performance)} may be begun only once; performance instances are not reusable.");
            hasBegun = true;
            var args = new ScenarioEventArgs(PerformanceIdentity, ScenarioHierarchy);
            Begin?.Invoke(this, args);
        }

        /// <inheritdoc/>
        public event EventHandler<ScenarioCompleteEventArgs> Complete;

        /// <inheritdoc/>
        public void CompletePerformance(bool? success)
        {
            if(hasCompleted) throw new InvalidOperationException($"An instance of {nameof(Performance)} may be completed only once; performance instances are not reusable.");
            hasBegun = hasCompleted = true;
            this.success = success;
            var args = new ScenarioCompleteEventArgs(PerformanceIdentity, ScenarioHierarchy, success);
            Complete?.Invoke(this, args);
        }

        /// <inheritdoc/>
        public bool Equals(Performance other)
        {
            if(ReferenceEquals(other, this)) return true;
            if(ReferenceEquals(other, null)) return false;

            return Equals(PerformanceIdentity, other.PerformanceIdentity);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj) => Equals(obj as Performance);

        /// <inheritdoc/>
        public override int GetHashCode() => PerformanceIdentity.GetHashCode();

        /// <inheritdoc/>
        public void Dispose()
        {
            if (ServiceProvider is IDisposable disposable) disposable.Dispose();
        }

        /// <summary>Initialises a new instance of <see cref="Performance"/></summary>
        /// <param name="serviceProvider">A dependency injection service provider</param>
        /// <param name="scenarioHierarchy">A collection of identifiers and names providing the scenario
        /// hierarchy; see <see cref="IHasScenarioHierarchy"/> for more information.</param>
        /// <param name="performanceIdentity">A unique identifier for the performance; if omitted (equal to <see cref="Guid.Empty"/>)
        /// then a new Guid will be generated as the identity for this performance</param>
        /// <exception cref="ArgumentNullException">If <paramref name="serviceProvider"/> is <see langword="null" /></exception>
        public Performance(IServiceProvider serviceProvider, IList<IdentifierAndName> scenarioHierarchy = default, Guid performanceIdentity = default)
        {
            ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            ScenarioHierarchy = scenarioHierarchy?.ToList() ?? new List<IdentifierAndName>();
            PerformanceIdentity = performanceIdentity != default ? performanceIdentity : Guid.NewGuid();
        }
    }
}