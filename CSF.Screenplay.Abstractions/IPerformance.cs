using System;
using System.Collections.Generic;
using CSF.Screenplay.Performances;

namespace CSF.Screenplay
{
    /// <summary>A performance represents a self-contained scope of performables which typically results in overall success or failure.</summary>
    /// <remarks>
    /// <para>
    /// The performance provides a scope for a script involving one or more <see cref="Actor"/> instances, executing
    /// <xref href="PerformableGlossaryItem?text=one+or+more+performable+items"/>.
    /// A Screenplay will contain one or more performances.
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
    /// Within a DI scope, the performance is an injectable service.
    /// You may wish to read a <xref href="HowScreenplayAndPerformanceRelateArticle?text=diagram+showing+how+screenplays,+performances,+actors+and+performables+relate+to+one+another" />.
    /// </para>
    /// </remarks>
    public interface IPerformance : IHasPerformanceIdentity,
                                    IHasServiceProvider,
                                    IDisposable,
                                    IBeginsAndEndsPerformance
    {
        /// <summary>Gets an ordered list of identifiers which indicate the current performance's name within an organisational hierarchy.</summary>
        /// <remarks>
        /// <para>
        /// A Screenplay typically contains more than one performance and may contain many.
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
        List<IdentifierAndName> NamingHierarchy { get; }

        /// <summary>Gets a value which indicates the state of the current performance.</summary>
        /// <seealso cref="Performances.PerformanceState"/>
        PerformanceState PerformanceState { get; }
    }
}
