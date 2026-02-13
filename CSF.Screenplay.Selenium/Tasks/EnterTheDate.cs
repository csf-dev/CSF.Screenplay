using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Selenium.Elements;
using OpenQA.Selenium;
using static CSF.Screenplay.Selenium.PerformableBuilder;

namespace CSF.Screenplay.Selenium.Tasks
{
    /// <summary>
    /// A Screenplay task for entering a value into an <c>&lt;input type="date"&gt;</c> element.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Use this task with the builder method <see cref="PerformableBuilder.EnterTheDate(DateTime?)"/>.
    /// This builder will guide you through picking the element into which the date should be entered.
    /// </para>
    /// <para>
    /// The rationale for this task is that web browsers or WebDriver implementations are inconsistent in the manner in which
    /// they support interacting with Date input elements.
    /// Browsers which require a JavaScript workaround are marked with the browser quirk
    /// <see cref="BrowserQuirks.CannotSetInputTypeDateWithSendKeys"/>.
    /// See the documentation for that quirk for more information.
    /// This task provides date-setting capabilities in a cross-browser manner.
    /// </para>
    /// <para>
    /// For browsers which are affected by the quirk, the date is set via a separate task: <see cref="SetTheElementValue"/>,
    /// with the 'simulate setting the value interactively' behaviour enabled.
    /// For browsers which are not affected by the quirk, the date is first cleared via the action <see cref="Actions.ClearTheContents"/>,
    /// and then keypresses are sent to the web browser to enter the date in a locale-specific format via <see cref="Actions.SendKeys"/>.
    /// </para>
    /// <para>
    /// Note that because entering a date interactively (by sending keys) requires a locale-specific format, that makes this task
    /// culture-sensitive.  It is important to ensure that the date value entered into the browser is entered using the same culture
    /// as which the browser is currently operating.  If the culture does not match then this could lead to mistakes in the value;
    /// consider muddling up US English and UK English dates.  In US English <c>04/05/2010</c> means the 5th April 2010.  In UK English
    /// that same date string means 4th May 2010, because the days and months are transposed in that format.
    /// If no culture information is specified in the constructor then this task defaults to the current culture: <see cref="CultureInfo.CurrentCulture"/>.
    /// That is often sufficient, particularly if the WebDriver is running locally on the same computer as is executing the Screenplay
    /// Performance. It could be problematic, though, if a Remote WebDriver is in use. A Remote WebDriver could be hosted in another
    /// nation and thus be operating under a different culture to the Screenplay Performance. In this case, be sure to specify the
    /// correct culture in the constructor of this task.
    /// </para>
    /// <para>
    /// If the date specified to this task is <see langword="null"/> then this task will clear the date from the target.
    /// </para>
    /// </remarks>
    /// <example>
    /// <para>
    /// A British English browser <c>en-GB</c> expects dates to be entered in the format ddMMyyyy.
    /// However, a US English browser <c>en-US</c> expects dates to be entered in the format MMddyyyy.
    /// </para>
    /// <para>The code sample below shows how to enter the date 5th April 2025 into a date input element with the id <c>due_date</c>,
    /// using the British English format.</para>
    /// <code>
    /// using CSF.Screenplay.Selenium.Elements;
    /// using static CSF.Screenplay.Selenium.PerformableBuilder;
    /// 
    /// readonly ITarget dueDate = new ElementId("due_date", "the due date");
    /// 
    /// // Within the logic of a custom task, deriving from IPerformable
    /// public async ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
    /// {
    ///     // ... other performance logic
    ///     await actor.PerformAsync(EnterTheDate(new DateTime(2025, 4, 5)).Into(dueDate).ForTheCultureNamed("en-GB"), cancellationToken);
    ///     // ... other performance logic
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="PerformableBuilder.EnterTheDate(DateTime?)"/>
    /// <seealso cref="BrowserQuirks.CannotSetInputTypeDateWithSendKeys"/>
    /// <seealso cref="SetTheElementValue"/>
    /// <seealso cref="Actions.ClearTheContents"/>
    /// <seealso cref="Actions.SendKeys"/>
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

        string FormatDateAsIso() => date.HasValue ? date.Value.ToString("yyyy-MM-dd") : string.Empty;

        /// <inheritdoc/>
        public ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            var browseTheWeb = actor.GetAbility<BrowseTheWeb>();
            if(browseTheWeb.WebDriver.HasQuirk(BrowserQuirks.CannotSetInputTypeDateWithSendKeys))
                return actor.PerformAsync(SetTheValueOf(target).To(FormatDateAsIso()).AsIfSetInteractively(), cancellationToken);

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