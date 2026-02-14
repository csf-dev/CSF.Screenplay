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
        /// Gets a Selenium <see cref="SelectElement"/> instance from the specified element.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <see cref="SelectElement"/> is a Selenium type which represents an HTML <c>&lt;select&gt;</c> element,
        /// wrapping the native <see cref="OpenQA.Selenium.IWebElement"/>.
        /// The select element class provides convenience functionality for reading and manipulating the state of the
        /// underlying element.
        /// </para>
        /// </remarks>
        /// <param name="element">A <see cref="SeleniumElement"/> which should represent an HTML <c>&lt;select&gt;</c> element</param>
        /// <returns>A Selenium select element</returns>
        /// <exception cref="UnexpectedTagNameException">If the element is not a <c>&lt;select&gt;</c> element</exception>
        /// <exception cref="ArgumentNullException">If the element is <see langword="null" /></exception>
        public static SelectElement AsSelectElement(this SeleniumElement element) => new SelectElement(element.WebElement);
    }
}