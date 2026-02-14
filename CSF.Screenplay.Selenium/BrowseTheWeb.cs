using System;
using CSF.Extensions.WebDriver;
using CSF.Extensions.WebDriver.Factories;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium
{
    /// <summary>
    /// Screenplay ability which allows an <see cref="Actor"/> to browse the web using a Selenium WebDriver.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The 'Browse the Web' ability wraps a <see cref="IWebDriver">Selenium WebDriver</see> and provides access to it via
    /// the <see cref="WebDriver"/> property. It also provide access to the options which were used to create the web driver,
    /// via the <see cref="DriverOptions"/> property.
    /// Following Selenium's architecture, both of these properties are abstract/interfaces and offer only the base/lowest common
    /// denominator types. Often, this is sufficient. Developers may check for other Selenium-related interfaces if they are required,
    /// using patterns such as the following:
    /// </para>
    /// <code>
    /// if(browseTheWeb.WebDriver is OpenQA.Selenium.ISupportsPrint printingDriver)
    ///     /* ... exercise printingDriver ... */
    /// </code>
    /// <para>
    /// This ability makes use of <xref href="UniversalWebDriverFactoryArticle?text=the+Universal+Web+Driver+Factory"/> from the
    /// <see href="https://github.com/csf-dev/CSF.Extensions.WebDriver">CSF.Extensions.WebDriver</see> package. This provides a
    /// configurable manner, using the <see href="https://learn.microsoft.com/en-us/dotnet/core/extensions/options">.NET Options
    /// pattern</see>, to specify the WebDriver without hard-coding the choice.
    /// </para>
    /// <para>
    /// The Selenium Extension to Screenplay makes the <see cref="IGetsWebDriver"/> web driver factory available via dependency injection.
    /// The recommended way in which to grant an Actor this ability is via an <see cref="IPersona"/>.  The web driver factory may be safely
    /// constructor-injected into the persona class.
    /// </para>
    /// </remarks>
    /// <example>
    /// <para>
    /// Imagine you have configuration like the following in your <c>appsettings.json</c>, following the
    /// <see href="https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options">Microsoft Options Pattern, with
    /// a configuration file</see>.
    /// </para>
    /// <code language="json">
    /// {
    ///   "WebDriverFactory": {
    ///     "DriverConfigurations": {
    ///       "LocalChrome": { "DriverType": "ChromeDriver" },
    ///       "LocalFirefox": { "DriverType": "FirefoxDriver" },
    ///     },
    ///   }
    /// }
    /// </code>
    /// <para>
    /// You could now use the following technique when you configure your <see cref="Actor"/> with this ability.
    /// The following example shows how to do this using an <see cref="IPersona"/>.
    /// </para>
    /// <para>
    /// In addition to the configuration above and the persona shown below, run this code whilst an <b>environment variable</b>
    /// named <c>WebDriverFactory__SelectedConfiguration</c> is defined and set to <c>LocalFirefox</c>.
    /// This environment variable selects which of the two configured WebDrivers is used (in this case, Firefox).
    /// Developers may use the configuration to store a library of available WebDriver configurations, and use a single
    /// environment variable to switch between them at execution time.
    /// </para>
    /// <code>
    /// using CSF.Extensions.WebDriver;
    /// using CSF.Screenplay.Selenium;
    /// 
    /// public class Webster(IGetsWebDriver webDriverFactory) : IPersona
    /// {
    ///     public string Name => "Webster";
    /// 
    ///     public Actor GetActor(Guid performanceIdentity)
    ///     {
    ///         var webster = new Actor(Name, performanceIdentity);
    ///         var browseTheWeb = new BrowseTheWeb(webDriverFactory);
    ///         webster.IsAbleTo(browseTheWeb);
    ///         return webster;
    ///     }
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="IWebDriver"/>
    /// <seealso cref="WebDriverAndOptions"/>
    /// <seealso cref="IGetsWebDriver"/>
    public class BrowseTheWeb : ICanReport, IDisposable
    {
        readonly Lazy<WebDriverAndOptions> webDriverAndOptions;
        bool disposedValue;

        /// <summary>
        /// Gets the Selenium WebDriver associated with the current ability instance.
        /// </summary>
        public IWebDriver WebDriver => webDriverAndOptions.Value.WebDriver;

        /// <summary>
        /// Gets the WebDriver options which were used to create <see cref="WebDriver"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// These options are for reference only; there is no effect on the <see cref="WebDriver"/> instance if you modify them.
        /// The purpose of this property is to allow developers to inspect the options which were used to create the WebDriver.
        /// </para>
        /// </remarks>
        public DriverOptions DriverOptions => webDriverAndOptions.Value.DriverOptions;

        static Lazy<WebDriverAndOptions> GetLazyWebDriverAndOptions(IGetsWebDriver webDriverFactory, string webDriverName)
        {
            if (webDriverFactory is null) throw new ArgumentNullException(nameof(webDriverFactory));

            return new Lazy<WebDriverAndOptions>(() =>
            {
                return webDriverName is null
                    ? webDriverFactory.GetDefaultWebDriver()
                    : webDriverFactory.GetWebDriver(webDriverName);
            });
        }

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} is able to browse the web using {BrowserName}",
                                actor.Name,
                                WebDriver.GetBrowserId()?.ToString() ?? "a Selenium WebDriver");

        /// <summary>
        /// Initializes a new instance of the <see cref="BrowseTheWeb"/> class.
        /// </summary>
        /// <remarks>
        /// <para>
        /// It is quite normal to omit the <paramref name="webDriverName"/> parameter, leaving it with its default <see langword="null"/> value.
        /// If the WebDriver name is omitted or null then the <see cref="IGetsWebDriver.GetDefaultWebDriver(Action{DriverOptions})"/> method will
        /// be used to get the WebDriver.  This requires that the WebDriver factory is configured with a default driver. This could be done via
        /// the <c>SelectedConfiguration</c> property of the JSON configuration, or via an environment variable (see the example code in the remarks
        /// to this class) or any other way which <see href="https://learn.microsoft.com/en-us/dotnet/core/extensions/configuration">the Microsoft
        /// Configuration Pattern</see> supports.
        /// Alternatively, to activate a specific named configuration, you may specify the WebDriver name here.
        /// </para>
        /// <para>
        /// It is normal to retrieve the <paramref name="webDriverFactory"/> parameter via Dependency Injection.  The Selenium Extension to
        /// Screenplay makes the factory available in that manner.
        /// </para>
        /// </remarks>
        /// <param name="webDriverFactory">A <see cref="IGetsWebDriver">universal WebDriver factory</see> instance</param>
        /// <param name="webDriverName">An optional name, specifying the WebDriver configuration (within those available in the factory) to use.</param>
        public BrowseTheWeb(IGetsWebDriver webDriverFactory, string webDriverName = null)
        {
            webDriverAndOptions = GetLazyWebDriverAndOptions(webDriverFactory, webDriverName);
        }

        /// <summary>
        /// Disposes the resources used by the <see cref="BrowseTheWeb"/> class.
        /// </summary>
        /// <param name="disposing">A boolean value indicating whether the method is called from the Dispose method.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing && webDriverAndOptions.IsValueCreated)
                {
                    webDriverAndOptions.Value.WebDriver.Quit();
                    webDriverAndOptions.Value.Dispose();
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