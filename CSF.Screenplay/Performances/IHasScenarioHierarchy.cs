using System.Collections.Generic;

namespace CSF.Screenplay.Performances
{
    /// <summary>An object which exposes a hierarchy of scenarios</summary>
    /// <remarks>
    /// <para>
    /// Integrations which consume Screenplay are commonly testing frameworks. Within these frameworks,
    /// each <see cref="Performance"/> corresponds to a test or scenario. Those tests/scenarios have names and/or
    /// unique identifiers. Likewise, those names &amp; identifiers are typically organised into a hierarchy,
    /// such as scenarios within features or test cases within fixtures.
    /// </para>
    /// <para>
    /// This interface provides a simple model by which to record which scenario, feature, test case, fixture (etc) the current
    /// performance belongs within, by representing each of these levels of the hierarchy as a <see cref="IdentifierAndName"/>.
    /// </para>
    /// </remarks>
    public interface IHasScenarioHierarchy
    {
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
        IReadOnlyList<IdentifierAndName> ScenarioHierarchy { get; }
    }
}