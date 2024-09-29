namespace CSF.Screenplay
{
    /// <summary>
    /// A part of a Screenplay performance which has a human-readable name.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Use this interface for any object within an <see cref="IPerformance"/> which could benefit from having a human-readable name.
    /// For example, static parameter values like Web API endpoints, web page URLs, elements on a web UI, dates/times in a calendar.
    /// By referring to an object by its name, and using that name in report-generating logic, reports generated from a Screenplay can
    /// become much easier to read and comprehend.
    /// </para>
    /// </remarks>
    public interface IHasName
    {
        /// <summary>
        /// Gets the human-readable name of the current object.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <see langword="null"/> is strongly discouraged here.  All types which implement this interface
        /// should return a non-null response from this property.
        /// </para>
        /// </remarks>
        string Name { get; }
    }
}
