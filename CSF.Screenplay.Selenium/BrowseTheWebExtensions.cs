using System;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium
{
    /// <summary>
    /// Extension methods for the <see cref="BrowseTheWeb"/> ability.
    /// </summary>
    public static class BrowseTheWebExtensions
    {
        /// <summary>
        /// Gets a JavaScript executor object based upon the current <see cref="WebDriver"/>.
        /// </summary>
        /// <returns>A JavaScript executor object</returns>
        /// <exception cref="NotSupportedException">If the current web driver does not support JavaScript execution.</exception>
        public static IJavaScriptExecutor GetJavaScriptExecutor(this BrowseTheWeb ability)
        {
            return ability.WebDriver is IJavaScriptExecutor executor
                ? executor
                : throw new NotSupportedException($"The web driver must implement {nameof(IJavaScriptExecutor)}");
        }

    }
}