namespace CSF.Screenplay
{
    /// <summary>
    /// A part of a Screenplay performance which has a human-readable name.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Use this interface for any object within an <see cref="IPerformance"/> which could benefit from having a human-readable name.
    /// For example, static parameter values like Web API endpoints, web page URLs or elements on a web UI.
    /// By referring to an object by its name, and using that name in report-generating logic, reports generated from a Screenplay can
    /// become much easier to read and comprehend.
    /// </para>
    /// <para>
    /// The <see cref="Name"/> property is used to provide a human-readable string which represents the object in the report text.
    /// This interface is a part of the mechanism for <xref href="ReportFormattingArticle?text=formatting+values+in+reports" /> in Screenplay.
    /// </para>
    /// </remarks>
    /// <seealso cref="Reporting.IFormattableValue"/>
    /// <seealso cref="Reporting.IValueFormatter"/>
    public interface IHasName
    {
        /// <summary>
        /// Gets the human-readable name of the current object.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <see langword="null"/> is strongly discouraged here.  All types which implement <see cref="IHasName"/>
        /// should return a non-null response from this property.
        /// </para>
        /// </remarks>
        string Name { get; }
    }
}
