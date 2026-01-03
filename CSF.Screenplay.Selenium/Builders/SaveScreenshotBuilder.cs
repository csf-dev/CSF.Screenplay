using System;
using CSF.Screenplay.Performables;
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
    /// Instead, use the <see cref="PerformableBuilder.SaveTheScreenshot(Screenshot)"/> method.
    /// </para>
    /// </remarks>
    public class SaveScreenshotBuilder : IGetsPerformable
    {
        readonly Screenshot screenshot;
        string name;
        ScreenshotImageFormat format = ScreenshotImageFormat.Png;

        /// <summary>
        /// Specifies a human-readable name/summary for the screenshot.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Specifying this name is recommended, as it will form a part of the report which is subsequently generated.
        /// The name will serve to identify the screenshot amongst other assets which may be included in the report.
        /// The name will also be used as part of the filename by which the screenshot asset file is saved.
        /// </para>
        /// </remarks>
        /// <param name="name">A human-readable name for the screenshot.</param>
        /// <returns>This same builder, so further customisations may be performed</returns>
        public SaveScreenshotBuilder WithTheName(string name)
        {
            this.name = name;
            return this;
        }

        /// <summary>
        /// Specifies the image format to use when saving the screenshot.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If the image format is omitted (this method is not used) then the saved screenshot will default to the Png image format.
        /// </para>
        /// </remarks>
        /// <param name="format">The file format</param>
        /// <returns>This same builder, so further customisations may be performed</returns>
        public SaveScreenshotBuilder WithTheFormat(ScreenshotImageFormat format)
        {
            this.format = format;
            return this;
        }

        IPerformable IGetsPerformable.GetPerformable() => new SaveScreenshot(screenshot, name, format);

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
        public static implicit operator SaveScreenshot(SaveScreenshotBuilder builder)
            => new SaveScreenshot(builder.screenshot, builder.name, builder.format);
    }
}