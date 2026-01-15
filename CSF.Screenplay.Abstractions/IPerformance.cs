using System;
using System.Collections.Generic;
using CSF.Screenplay.Performances;

namespace CSF.Screenplay
{
    /// <summary>A Performance corresponds to a self-contained scope of Performables, performed by Actors, which results in success or failure.</summary>
    /// <remarks>
    /// <para>
    /// In Screenplay, a performance is .NET logic involving one or more <see cref="Actor"/> instances, executing
    /// <xref href="PerformableGlossaryItem?text=one+or+more+performable+items"/>.
    /// A Screenplay will be comprised of one or more performances.
    /// </para>
    /// <para>
    /// In practice a performance object, deriving from this interface, often corresponds to a .NET method in some manner.
    /// The method might be regular application logic or it might be a Test/Scenario, using the semantics of an applicable testing framework.
    /// The performance object (deriving from this interface) provides a name for the method (the performance name) as well as tracking
    /// its running-state.
    /// When using BDD-style testing frameworks, there might be a single .NET method to which the performance is linked.
    /// Nevertheless, a performance defines an execution scope (with a beginning and end).  Even if that scope does not
    /// correspond neatly to a single method, it exists regardless.
    /// </para>
    /// <para>
    /// The body of a performance method typically creates one or more <see cref="Actor"/> via the <see cref="IStage"/> and then has the actor
    /// execute one or more performables.
    /// Particularly when using Screenplay for automated testing, these performables are organised into a beginning, middle and end,
    /// corresponding to the "Given, When, Then" design popular in testing.
    /// These three phases are included for use in Screenplay, declared in <see cref="Actors.PerformancePhase"/>.
    /// The use of performance phases is optional, although encouraged.
    /// </para>
    /// <para>
    /// Every performance method should return a result, indicating whether the performance was a success.
    /// A successful performance is one which progressed from beginning to end, and every step (performable) operated in the manner in which
    /// was expected.
    /// Additionally, when using Screenplay for software testing, a performance is a success if all its _assertions_ pass.
    /// A succesful performance should return <see langword="true" /> from its performance method.
    /// An unsuccessful one should return <see langword="false" />.
    /// </para>
    /// <para>
    /// As noted above, when Screenplay is being used for automated testing, the performance method corresponds directly to a single test.
    /// In the testing framework that might be called a "scenario", a "test", a "test case", or a "theory".
    /// When using Screenplay within a testing integration, the performance corresponds very closely to
    /// <xref href="ScenarioGlossaryItem?text=the+current+Scenario"/>.
    /// </para>
    /// <para>
    /// The object which implements this interface is the representation of the the method described above.
    /// One instance of this type - "the performance object" - corresponds to one execution of such a method.
    /// The performance object also corresponds to the lifetime of <xref href="DependencyInjectionScopeArticle?text=a+dependency+injection+scope"/>;
    /// a new scope is created for each performance.
    /// Within a DI scope, the performance object is an injectable service.
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
        IReadOnlyList<IdentifierAndName> NamingHierarchy { get; }

        /// <summary>Gets a value which indicates the state of the current performance.</summary>
        /// <seealso cref="Performances.PerformanceState"/>
        PerformanceState PerformanceState { get; }
    }
}
