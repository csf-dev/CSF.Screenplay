using System;
using CSF.Extensions.WebDriver;
using CSF.Extensions.WebDriver.Factories;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium
{
    /// <summary>
    /// Screenplay ability which allows an <see cref="Actor"/> to browse the web using a Selenium WebDriver.
    /// </summary>
    public class BrowseTheWeb : ICanReport, IDisposable
    {
        readonly IGetsWebDriver webDriverFactory;
        WebDriverAndOptions webDriverAndOptions;
        bool disposedValue;

        /// <summary>
        /// Gets the Selenium WebDriver associated with the current ability instance.
        /// </summary>
        public IWebDriver WebDriver
        {
            get {
                if(webDriverAndOptions is null)
                    webDriverAndOptions = webDriverFactory.GetDefaultWebDriver();

                return webDriverAndOptions.WebDriver;
            }
        }

        /// <summary>
        /// Gets the WebDriver options which were used to create <see cref="WebDriver"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// These options are for reference only; there is no effect on the <see cref="WebDriver"/> instance if you modify them.
        /// The purpose of this property is to allow developers to inspect the options which were used to create the WebDriver.
        /// </para>
        /// </remarks>
        public DriverOptions DriverOptions
        {
            get {
                if(webDriverAndOptions is null)
                    webDriverAndOptions = webDriverFactory.GetDefaultWebDriver();

                return webDriverAndOptions.DriverOptions;
            }
        }

        /// <summary>
        /// Gets a JavaScript executor object based upon the current <see cref="WebDriver"/>.
        /// </summary>
        /// <returns>A JavaScript executor object</returns>
        /// <exception cref="NotSupportedException">If the current web driver does not support JavaScript execution.</exception>
        public IJavaScriptExecutor GetJavaScriptExecutor()
        {
            return WebDriver is IJavaScriptExecutor executor
                ? executor
                : throw new NotSupportedException($"The web driver must implement {nameof(IJavaScriptExecutor)}");
        }

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} is able to browse the web using {BrowserName}",
                                actor.Name,
                                WebDriver.GetBrowserId()?.ToString() ?? "a Selenium WebDriver");

        /// <summary>
        /// Initializes a new instance of the <see cref="BrowseTheWeb"/> class.
        /// </summary>
        /// <param name="webDriverFactory"></param>
        public BrowseTheWeb(IGetsWebDriver webDriverFactory)
        {
            this.webDriverFactory = webDriverFactory ?? throw new ArgumentNullException(nameof(webDriverFactory));
        }

        /// <summary>
        /// Disposes the resources used by the <see cref="BrowseTheWeb"/> class.
        /// </summary>
        /// <param name="disposing">A boolean value indicating whether the method is called from the Dispose method.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    webDriverAndOptions?.Dispose();
                }
                disposedValue = true;
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}