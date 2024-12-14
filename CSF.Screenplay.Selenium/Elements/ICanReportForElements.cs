using System;
using CSF.Screenplay.Selenium.Questions;

namespace CSF.Screenplay.Selenium.Elements
{
    /// <summary>
    /// Similar to <see cref="ICanReport"/> but provides access to a collection of Selenium elements to augment the report data.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This interface is often combined with either <see cref="IElementCollectionPerformableWithResult{TResult}"/>
    /// for specialised performables which interact with collections of Selenium elements.
    /// </para>
    /// </remarks>
    public interface ICanReportForElements
    {
        /// <summary>
        /// Counterpart to <see cref="ICanReport.GetReportFragment(IHasName, IFormatsReportFragment)"/> except that this method also offers a
        /// Selenium element collection.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Please see the documentation for <see cref="ICanReport.GetReportFragment(IHasName, IFormatsReportFragment)"/> for more information.
        /// </para>
        /// </remarks>
        /// <param name="actor">An actor for whom to write the report fragment</param>
        /// <param name="formatter">A report-formatting service</param>
        /// <param name="elements">The Selenium elements for which the report is being written</param>
        /// <returns>A human-readable report fragment.</returns>
        ReportFragment GetReportFragment(IHasName actor, Lazy<SeleniumElementCollection> elements, IFormatsReportFragment formatter);
    }
}