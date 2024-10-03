using System;

namespace CSF.Screenplay
{
    /// <summary>
    /// A service which produces formatted report fragments from a template and a collection of parameter values.
    /// </summary>
    public interface IFormatsReportFragment
    {
        /// <summary>
        /// Gets the formatted report fragment from the specified template and values.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The <paramref name="template"/> should be a human-readable string (localized if you wish) which would be recorded in a
        /// Screenplay report.
        /// This string may contain any number of placeholder markers which are indicated by some text enclosed within
        /// braces, such as <c>{Name}</c>.
        /// This functions in a slightly similar fashion to .NET logging:
        /// <see href="https://learn.microsoft.com/en-us/dotnet/core/extensions/logging?tabs=command-line#log-message-template"/>
        /// or the <see cref="string.Format(string, object[])"/> method.
        /// </para>
        /// <para>
        /// Importantly, the placeholders are identified by names, rather than numeric index, in the same way that logging template
        /// strings work.  Placeholders do not support any kind of format or alignment syntax.
        /// </para>
        /// <para>
        /// Developers are encouraged to choose meaningful names for their placeholders.
        /// The <see cref="ReportFragment"/> which is produced by this method will include the <paramref name="values"/>
        /// associated with those placeholder names.
        /// This allows reporting infrastructure to work in a similar manner to the way in which structured logging is described in
        /// the article linked above.
        /// This provides a richer data model than simply a plain string.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// The following code will produce the final formatted string as noted below.
        /// </para>
        /// <code>
        /// var p1 = "first";
        /// var p2 = "second";
        /// formatter.Format("The values are {p2} and {p1}", p1, p2);
        /// 
        /// // This will yield the result "The values are first and second"
        /// </code>
        /// </example>
        /// <param name="template">A string template for the report fragment</param>
        /// <param name="values">A collection of values associated with the report fragment</param>
        /// <returns>A formatted report fragment</returns>
        /// <seealso cref="ReportFragment"/>
        ReportFragment Format(string template, params object[] values);
    }
}