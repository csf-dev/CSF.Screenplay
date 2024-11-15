using System;
using CSF.Screenplay.Selenium.Elements;

namespace CSF.Screenplay.Selenium.Actions
{
    /// <summary>
    /// Similar to <see cref="ICanReport"/> but provides access to a Selenium element to augment the report data.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This interface is often combined with either <see cref="ISingleElementPerformable"/> or <see cref="ISingleElementPerformableWithResult{T}"/>,
    /// for specialised performables which interact with a single Selenium element.
    /// </para>
    /// </remarks>
    public interface ICanReportForElement
    {
        /// <summary>
        /// Counterpart to <see cref="ICanReport.GetReportFragment(IHasName, IFormatsReportFragment)"/> except that this method also offers a
        /// Selenium element.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Please see the documentation for <see cref="ICanReport.GetReportFragment(IHasName, IFormatsReportFragment)"/> for more information.
        /// </para>
        /// </remarks>
        /// <param name="actor">An actor for whom to write the report fragment</param>
        /// <param name="formatter">A report-formatting service</param>
        /// <param name="element">The Selenium element for which the report is being written</param>
        /// <returns>A human-readable report fragment.</returns>
        ReportFragment GetReportFragment(IHasName actor, Lazy<SeleniumElement> element, IFormatsReportFragment formatter);
    }
}