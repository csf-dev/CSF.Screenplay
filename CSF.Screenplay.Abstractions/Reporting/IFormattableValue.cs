namespace CSF.Screenplay.Reporting
{
    /// <summary>
    /// An object which has its own functionality for generating a human-readable representation of itself for a Screenplay report.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Implement this interface in your own types in order to provide a custom representation of the object when it is included in
    /// a Screenplay report.
    /// The <see cref="FormatForReport"/> method should be used to create a human-readable string which represents the object in the report text.
    /// This interface is a part of the mechanism for <xref href="ReportFormattingArticle?text=formatting+values+in+reports" /> in Screenplay.
    /// </para>
    /// </remarks>
    /// <seealso cref="IHasName"/>
    /// <seealso cref="IValueFormatter"/>
    public interface IFormattableValue
    {
        /// <summary>
        /// Gets a human-readable formatted string which represents the current object instance, suitable to be used in a Screenplay report.
        /// </summary>
        /// <returns>A formatted string which represents the current instance.</returns>
        string FormatForReport();
    }
}