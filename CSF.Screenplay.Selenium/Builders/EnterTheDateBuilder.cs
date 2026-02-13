using System;
using System.Globalization;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Actions;
using CSF.Screenplay.Selenium.Elements;
using CSF.Screenplay.Selenium.Tasks;

namespace CSF.Screenplay.Selenium.Builders
{
    /// <summary>
    /// A builder type which creates an instance of <see cref="EnterTheDate"/>.
    /// </summary>
    public class EnterTheDateBuilder : IGetsPerformable
    {
        readonly DateTime? date;
        ITarget target;
        CultureInfo culture;

        /// <summary>
        /// Specifies the target element into which to enter the date.  This must be an <c>&lt;input type="date"&gt;</c> element.
        /// </summary>
        /// <param name="target">The target element</param>
        /// <returns>This same builder, so calls may be chained</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="target"/> is null</exception>
        /// <exception cref="InvalidOperationException">If this method is used more than once</exception>
        public EnterTheDateBuilder Into(ITarget target)
        {
            if (target is null)
                throw new ArgumentNullException(nameof(target));
            if(this.target != null)
                throw new InvalidOperationException("The target has already been set; it may not be set again.");
            
            this.target = target;
            return this;
        }

        /// <summary>
        /// Specifies the culture for which to enter the date.  This must be the culture in which the web browser is operating.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Web browser are culture-aware applications and they will render the input/display value of a date field using the culture
        /// in which their operating system is configured.  This impacts the manner in which users input dates.
        /// If this method is not used, the task returned by this builder will use the culture of the operating system/environment
        /// that is executing the Screenplay Performance. This is usually OK when running the web browser locally, but it might not match
        /// the browser's culture when using remote web browsers.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// For example, a British English browser <c>en-GB</c> expects dates to be entered in the format ddMMyyyy.
        /// However, a US English browser <c>en-US</c> expects dates to be entered in the format MMddyyyy.
        /// </para>
        /// <para>
        /// The <paramref name="cultureIdentifier"/> parameter of this method must be the culture identifier of the culture which the
        /// browser is operating under, such as <c>en-GB</c>.
        /// </para>
        /// </example>
        /// <param name="cultureIdentifier">A culture identifier string</param>
        /// <returns>This same builder, so calls may be chained</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="cultureIdentifier"/> is null</exception>
        /// <exception cref="CultureNotFoundException">If <paramref name="cultureIdentifier"/> indicates a culture which is not found</exception>
        public EnterTheDateBuilder ForTheCultureNamed(string cultureIdentifier)
        {
            culture = CultureInfo.GetCultureInfo(cultureIdentifier);
            return this;
        }

        /// <inheritdoc/>
        public IPerformable GetPerformable() => new EnterTheDate(date, target, culture);

        /// <summary>
        /// Initializes a new instance of the <see cref="EnterTheDateBuilder"/> class with the specified date.
        /// </summary>
        /// <param name="date">The date to enter, or null</param>
        public EnterTheDateBuilder(DateTime? date)
        {
            this.date = date;
        }
    }
}