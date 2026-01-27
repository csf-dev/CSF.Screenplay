using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Selenium.Elements;
using static CSF.Screenplay.Selenium.PerformableBuilder;

namespace CSF.Screenplay.Selenium.Actions
{
    /// <summary>
    /// A <see cref="IPerformable"/> which represents an actor entering a date value into an <c>&lt;input type="date"&gt;</c> element.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Note that this task is culture-sensitive.  Ensure that the date value is entered into the browser using the culture in which the browser is
    /// running.
    /// If no culture information is specified then this task defaults to the current culture: <see cref="CultureInfo.CurrentCulture"/>.
    /// However, this is not certain to be correct, particularly in remote/cloud configurations where the web browser is operating on different
    /// infrastructure to the computer which is executing the Screenplay performance.  These two computers might be operating in different cultures.
    /// </para>
    /// <para>
    /// If the date specified to this task is <see langword="null"/> then this task will clear the date from the target.
    /// </para>
    /// </remarks>
    /// <example>
    /// <para>
    /// For example, a British English browser <c>en-GB</c> expects dates to be entered in the format ddMMyyyy.
    /// However, a US English browser <c>en-US</c> expects dates to be entered in the format MMddyyyy.
    /// </para>
    /// </example>
    public class EnterTheDate : IPerformable, ICanReport
    {
        const string nonNumericPattern = @"\D";
        static readonly Regex nonNumeric = new Regex(nonNumericPattern,
                                                     RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant,
                                                     TimeSpan.FromMilliseconds(100));

        readonly DateTime? date;
        readonly ITarget target;
        readonly CultureInfo culture;

        string GetShortDatePattern() => culture.DateTimeFormat.ShortDatePattern;

        string FormatDate() => date.HasValue ? date.Value.ToString(GetShortDatePattern()) : null;

        /// <inheritdoc/>
        public ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            if(!date.HasValue)
                return actor.PerformAsync(ClearTheContentsOf(target), cancellationToken);

            var dateText = nonNumeric.Replace(FormatDate(), string.Empty);
            return actor.PerformAsync(EnterTheText(dateText).Into(target), cancellationToken);
        }

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
        {
            return date.HasValue
                ? formatter.Format("{Actor} enters the date {Date} into {Target}", actor.Name, FormatDate(), target)
                : formatter.Format("{Actor} clears the date from {Target}", actor.Name, date, target);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnterTheDate"/> class with the specified date.
        /// </summary>
        /// <param name="date">The date to enter into the element.</param>
        /// <param name="target">The element into which to enter the data</param>
        /// <param name="culture">The culture for which to enter the date</param>
        public EnterTheDate(DateTime? date, ITarget target, CultureInfo culture = null)
        {
            this.date = date;
            this.target = target ?? throw new ArgumentNullException(nameof(target));
            this.culture = culture ?? CultureInfo.CurrentCulture;
        }
    }
}