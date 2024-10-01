using System;
using System.Collections.Generic;

namespace CSF.Screenplay.Reporting
{
    /// <summary>
    /// A registry of the concrete types of <see cref="IFormatter"/> which are available for use by the Screenplay reporting functionality.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Formatter types stored within this registry will be selected for use in the reverse order in which they appear in the collection.
    /// When selecting a formatter for a particular object/value, this collection will be iterated starting at the end, moving toward the
    /// beginning.
    /// This means that (presuming new formatter types are added using <see cref="ICollection{T}.Add(T)"/>), types which are added later will
    /// be used with precedence over those which are added earlier.
    /// This is the mechanism which is used as a tie-breaker if two or more <see cref="IFormatter"/> types would both return <see langword="true" />
    /// from <see cref="IFormatter.CanFormat(object)"/>.
    /// </para>
    /// <para>
    /// Developers might want to add a <see cref="IFormatter"/> type to this collection, which is to have a lower precedence than another which has already
    /// been added, when they are both able to format the same value.
    /// In that scenario, use <see cref="IList{T}.Insert(int, T)"/> to explicitly add the formatter at a lower index (closer to the start of
    /// the collection).
    /// </para>
    /// </remarks>
    public interface IFormatterRegistry : IList<Type> {}
}