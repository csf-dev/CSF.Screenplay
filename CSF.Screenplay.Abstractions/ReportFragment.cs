using System.Collections.Generic;
using System.Collections.ObjectModel;
using CSF.Screenplay.Reporting;

namespace CSF.Screenplay
{
    /// <summary>
    /// A model for a fragment of a report upon a <see cref="IPerformance"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// A report fragment is typically human readable summary of the execution of a single <xref href="PerformableGlossaryItem?text=performable+item"/>
    /// or the assignment of an <xref href="AbilityGlossaryItem?text=ability"/> to an <see cref="Actor"/> (the actor "gaining" an ability).
    /// A complete report about an <see cref="IPerformance"/> would usually contain many such fragments, organised hierarchically where performables
    /// consume &amp; execute performables themselves.  This creates a nested structure where the outermost report fragments describe higher-level
    /// performables, with lower-level performables contained within.
    /// </para>
    /// <para>
    /// Report fragments are created from a template, which is a human-readable string with named placeholders, using a syntax which is very similar
    /// to the way in which Microsoft.Extensions.Logging uses template strings: <see href="https://learn.microsoft.com/en-us/dotnet/core/extensions/logging"/>.
    /// The template may have any number of placeholders, which are names enclosed within braces, such as <c>{Actor}</c>.
    /// These placeholders are then populated with a collection of values which are provided to fill those placeholders.
    /// The result is a completely formatted, human-readable report fragment string.
    /// </para>
    /// <para>
    /// This model holds the result of this formatting process, the implementation of <see cref="IFormatsReportFragment"/> is responsible for
    /// performing the formatting itself.
    /// </para>
    /// </remarks>
    /// <example>
    /// <para>
    /// An example of a template string is <c>"{Actor} washes {Count} dishes"</c>.
    /// This template expects two placeholder values for <c>Actor</c> &amp; <c>Count</c>.
    /// A sample <see cref="FormattedFragment"/> which might result from this could be <c>"Joe washes 5 dishes"</c>.
    /// </para>
    /// </example>
    /// <seealso cref="IFormatsReportFragment"/>
    public sealed class ReportFragment
    {
        /// <summary>
        /// Gets the original template string for this report fragment, without placeholder substitution.
        /// </summary>
        public string OriginalTemplate { get; }

        /// <summary>
        /// Gets the formatted report fragment, after placeholder substitution has taken place.
        /// </summary>
        public string FormattedFragment { get; }

        /// <summary>
        /// Gets a collection of the placeholder values, and the placeholder names to which those values correspond.
        /// </summary>
        public IReadOnlyList<NameAndValue> PlaceholderValues { get; }

        /// <inheritdoc/>
        public override string ToString() => FormattedFragment;

        /// <summary>
        /// Initializes a new instance of <see cref="ReportFragment"/>.
        /// </summary>
        /// <param name="originalTemplate">The original template string</param>
        /// <param name="formattedFragment">The formatted report fragment</param>
        /// <param name="placeholderValues">The placeholder values</param>
        /// <exception cref="System.ArgumentNullException">If any parameter is <see langword="null" />.</exception>
        public ReportFragment(string originalTemplate, string formattedFragment, IReadOnlyList<NameAndValue> placeholderValues)
        {
            OriginalTemplate = originalTemplate ?? throw new System.ArgumentNullException(nameof(originalTemplate));
            FormattedFragment = formattedFragment ?? throw new System.ArgumentNullException(nameof(formattedFragment));
            PlaceholderValues = placeholderValues ?? throw new System.ArgumentNullException(nameof(placeholderValues));
        }
    }
}