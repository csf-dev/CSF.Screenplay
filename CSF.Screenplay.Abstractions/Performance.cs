using System;
using System.Collections.Generic;
using System.Linq;
using CSF.Screenplay.Performances;

namespace CSF.Screenplay
{
    /// <summary>A performance represents a self-contained scope of performables which typically results in overall success or failure.</summary>
    /// <remarks>
    /// <para>
    /// The performance provides a scope for a script involving one or more <see cref="Actor"/> instances, executing
    /// <xref href="PerformableGlossaryItem?text=one+or+more+performable+items"/>.
    /// A <see cref="Screenplay"/> will contain one or more performances.
    /// A performance is a series of actions/performables with a beginning, middle and end.
    /// Actors participate in the performance and drive the performables forward.
    /// Typically a performance will complete in either success or failure.
    /// </para>
    /// <para>
    /// Where Screenplay is being used for automated testing, a performance corresponds to a single test.
    /// In the testing framework that might be called a "scenario", a "test", a "test case", or a "theory".
    /// When using Screenplay within a testing integration, the performance corresponds very closely to
    /// <xref href="ScenarioGlossaryItem?text=the+current+Scenario"/>.
    /// </para>
    /// <para>
    /// The performance object also corresponds to the lifetime of <xref href="DependencyInjectionScopeArticle?text=the+dependency+injection+scope"/>.
    /// A new scope is created for each performance.
    /// You may wish to read a <xref href="HowScreenplayAndPerformanceRelateArticle?text=diagram+showing+how+screenplays,+performances,+actors+and+performables+relate+to+one+another" />.
    /// </para>
    /// </remarks>
    public abstract class Performance : IHasPerformanceIdentity,
                                        IHasServiceProvider,
                                        IEquatable<Performance>,
                                        IDisposable,
                                        IBeginsAndEndsPerformance
    {        
        bool hasBegun, hasCompleted;
        bool? success;

        /// <inheritdoc/>
        public Guid PerformanceIdentity { get; }

        /// <inheritdoc/>
        public IServiceProvider ServiceProvider { get; }

        /// <summary>Gets an ordered list of identifiers which indicate the current performance's name within an organisational hierarchy.</summary>
        /// <remarks>
        /// <para>
        /// A <see cref="Screenplay"/> typically contains more than one performance and may contain many.
        /// It is normal to organise performances into a hierarchical structure based upon their purpose, role or relationship.
        /// The position of the current performance in that naming structure is represented by the value of this property.
        /// </para>
        /// <para>
        /// The ordered list of <see cref="IdentifierAndName"/> instances indicate a path from the 'root' of the hierarchy
        /// (which has no inherent name) to the current performance. Identifier/name pairs which are earlier in the collection
        /// are considered to be closer to the root, whereas latter identifier/names are branch &amp; leaf names.
        /// In this manner, they work very similarly to .NET namespaces.
        /// The earlier in the list that a name appears, the more general it should be, representing a wider category.
        /// </para>
        /// <para>
        /// When using Screenplay with <xref href="IntegrationGlossaryItem?an+automated+testing+integration"/>, this hierarchy of names
        /// would typically correspond to the naming convention used by the testing framework.
        /// That might be based upon .NET namespaces, classes and test methods for a more traditional unit testing framework.
        /// Alternatively, for a BDD-style testing framework, it could be named based upon human-readable
        /// feature &amp; scenario names.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// If the current performance is to be named <c>Joe can take out the Trash</c>, and it is part of a parent name, named
        /// <c>Joe can do his chores</c> then the first identifier in the list will be named <c>Joe can do his chores</c>
        /// and the second will be named <c>Joe can take out the Trash</c>.
        /// </para>
        /// </example>
        public List<IdentifierAndName> NamingHierarchy { get; } = new List<IdentifierAndName>();

        /// <summary>Gets a value which indicates the state of the current performance.</summary>
        /// <seealso cref="Performances.PerformanceState"/>
        public PerformanceState PerformanceState
        {
            get {
                if (!hasBegun) return PerformanceState.NotStarted;
                if (!hasCompleted) return PerformanceState.InProgress;
                switch(success)
                {
                    case true: return PerformanceState.Success;
                    case false: return PerformanceState.Failed;
                    default: return PerformanceState.Completed;
                }
            }
        }

        /// <inheritdoc/>
        public void BeginPerformance()
        {
            if(hasBegun) throw new InvalidOperationException($"An instance of {nameof(Performance)} may be begun only once; performance instances are not reusable.");
            hasBegun = true;
            InvokePerformanceBegun();
        }

        /// <summary>
        /// Invokes an event which signifies that a performance has begun.
        /// </summary>
        protected abstract void InvokePerformanceBegun();

        /// <inheritdoc/>
        public void FinishPerformance(bool? success)
        {
            if (!hasBegun) throw new InvalidOperationException($"An instance of {nameof(Performance)} may not be completed before it has begun.");
            if(hasCompleted) throw new InvalidOperationException($"An instance of {nameof(Performance)} may be completed only once; performance instances are not reusable.");
            hasBegun = hasCompleted = true;
            this.success = success;
            InvokePerformanceFinished(success);
        }

        /// <summary>
        /// Invokes an event which signifies that a performance has completed.
        /// </summary>
        /// <param name="success">A value which indicates whether or not the performance was a success.</param>
        protected abstract void InvokePerformanceFinished(bool? success);

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
        /// <param name="namingHierarchy">A collection of identifiers and names providing the hierarchical name of this
        /// performance; see <see cref="NamingHierarchy"/> for more information.</param>
        /// <param name="performanceIdentity">A unique identifier for the performance; if omitted (equal to <see cref="Guid.Empty"/>)
        /// then a new Guid will be generated as the identity for this performance</param>
        /// <exception cref="ArgumentNullException">If <paramref name="serviceProvider"/> is <see langword="null" /></exception>
        protected Performance(IServiceProvider serviceProvider, IList<IdentifierAndName> namingHierarchy, Guid performanceIdentity)
        {
            ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            NamingHierarchy = namingHierarchy?.ToList() ?? new List<IdentifierAndName>();
            PerformanceIdentity = performanceIdentity != default ? performanceIdentity : Guid.NewGuid();
        }
    }
}