using System;
using OpenQA.Selenium.Support.UI;

namespace CSF.Screenplay.Selenium.Elements
{
    /// <summary>
    /// Extension methods for <see cref="SeleniumElement"/>.
    /// </summary>
    public static class SeleniumElementExtensions
    {
        /// <summary>
        /// Gets a <see cref="SelectElement"/> instance from the specified element.
        /// </summary>
        /// <remarks>
        /// <para>
        /// A <c>SelectElement</c> is a Selenium object which represents an HTML <c>&lt;select&gt;</c> element, providing
        /// convenience functionality for reading and manipulating its state.
        /// </para>
        /// </remarks>
        /// <param name="element">A Selenium element</param>
        /// <returns>A Selenium select element</returns>
        /// <exception cref="UnexpectedTagNameException">If the element is not a <c>&lt;select&gt;</c> element</exception>
        /// <exception cref="ArgumentNullException">If the element is <see langword="null" /></exception>
        public static SelectElement AsSelectElement(this SeleniumElement element) => new SelectElement(element.WebElement);
    }
}