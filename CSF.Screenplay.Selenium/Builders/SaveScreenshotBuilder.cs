using System;
using CSF.Screenplay.Selenium.Actions;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Builders
{
    /// <summary>
    /// A builder class to create an instance of <see cref="SaveScreenshot"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Like all builders in this namespace, this class is not to be instantiated directly.
    /// Instead, use the <see cref="SeleniumPerformableBuilder.SaveTheScreenshot(Screenshot)"/> method.
    /// </para>
    /// </remarks>
    public class SaveScreenshotBuilder
    {
        readonly Screenshot screenshot;

        /// <summary>
        /// Specifies a short name for the screenshot, to help identify it in the report.
        /// </summary>
        /// <param name="name">A short name for the screenshot</param>
        /// <returns>A performable</returns>
        public IPerformable WithTheName(string name) => new SaveScreenshot(screenshot, name);

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveScreenshotBuilder"/> class.
        /// </summary>
        /// <param name="screenshot">The Selenium screenshot instance.</param>
        internal SaveScreenshotBuilder(Screenshot screenshot)
        {
            this.screenshot = screenshot ?? throw new ArgumentNullException(nameof(screenshot));
        }

        /// <summary>
        /// Implicitly converts a <see cref="SaveScreenshotBuilder"/> to an <see cref="SaveScreenshot"/>.
        /// </summary>
        /// <param name="builder">The <see cref="SaveScreenshotBuilder"/> instance.</param>
        /// <returns>An <see cref="SaveScreenshot"/> instance.</returns>
        public static implicit operator SaveScreenshot(SaveScreenshotBuilder builder) => new SaveScreenshot(builder.screenshot);
    }
}