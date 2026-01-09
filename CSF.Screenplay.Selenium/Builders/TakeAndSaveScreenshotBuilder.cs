using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Actions;
using CSF.Screenplay.Selenium.Tasks;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Builders
{
    /// <summary>
    /// A builder class to create an instance of <see cref="TakeAndSaveScreenshot"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Like all builders in this namespace, this class is not to be instantiated directly.
    /// Instead, use either of the <see cref="PerformableBuilder.TakeAndSaveAScreenshot()"/>
    /// or <see cref="PerformableBuilder.TakeAndSaveAScreenshotIfSupported()"/>
    /// methods.
    /// </para>
    /// </remarks>
    public class TakeAndSaveScreenshotBuilder : IGetsPerformable
    {
        readonly bool throwIfUnsupported;
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
        public TakeAndSaveScreenshotBuilder WithTheName(string name)
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
        public TakeAndSaveScreenshotBuilder WithTheFormat(ScreenshotImageFormat format)
        {
            this.format = format;
            return this;
        }

        IPerformable IGetsPerformable.GetPerformable() => new TakeAndSaveScreenshot(name, format, throwIfUnsupported);

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveScreenshotBuilder"/> class.
        /// </summary>
        /// <param name="throwIfUnsupported">If set to <see langword="false"/> then this performance will not raise an
        /// exception if the <see cref="BrowseTheWeb"/> ability does not support taking screenshots.  Otherwise it will.</param>
        internal TakeAndSaveScreenshotBuilder(bool throwIfUnsupported)
        {
            this.throwIfUnsupported = throwIfUnsupported;
        }

        /// <summary>
        /// Implicitly converts a <see cref="SaveScreenshotBuilder"/> to an <see cref="SaveScreenshot"/>.
        /// </summary>
        /// <param name="builder">The <see cref="SaveScreenshotBuilder"/> instance.</param>
        /// <returns>An <see cref="SaveScreenshot"/> instance.</returns>
        public static implicit operator TakeAndSaveScreenshot(TakeAndSaveScreenshotBuilder builder)
            => new TakeAndSaveScreenshot(builder.name, builder.format, builder.throwIfUnsupported);
    }
}