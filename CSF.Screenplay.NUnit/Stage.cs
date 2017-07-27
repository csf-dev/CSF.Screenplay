using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Reporting;
using CSF.Screenplay.Web.Abilities;
using CSF.WebDriverFactory;
using OpenQA.Selenium;

namespace CSF.Screenplay.NUnit
{
  /// <summary>
  /// A service locator type for Screenplay-related objects.
  /// </summary>
  public static class Stage
  {
    static IReporter reporter;
    static ICast cast;
    static IWebDriver webDriver;
    static IUriTransformer uriTransformer;
    static IWebDriverFactoryProvider webDriverFactoryProvider;

    /// <summary>
    /// Gets or sets the current reporter.
    /// </summary>
    /// <value>The current reporter.</value>
    public static IReporter Reporter
    {
      get { return reporter; }
      set {
        if(value == null)
          throw new ArgumentNullException(nameof(value));

        reporter = value;
      }
    }

    /// <summary>
    /// Gets or sets the current cast implementation.
    /// </summary>
    /// <value>The cast.</value>
    public static ICast Cast
    {
      get { return cast; }
      set {
        if(value == null)
          throw new ArgumentNullException(nameof(value));

        cast = value;
      }
    }

    /// <summary>
    /// Gets or sets the current URI transformer.
    /// </summary>
    /// <value>The URI transformer.</value>
    public static IUriTransformer UriTransformer
    {
      get { return uriTransformer; }
      set {
        if(value == null)
          throw new ArgumentNullException(nameof(value));
        
        uriTransformer = value;
      }
    }

    /// <summary>
    /// Gets or sets the current web driver factory provider.
    /// </summary>
    /// <value>The web driver factory provider.</value>
    public static IWebDriverFactoryProvider WebDriverFactoryProvider
    {
      get { return webDriverFactoryProvider; }
      set {
        if(value == null)
          throw new ArgumentNullException(nameof(value));
        
        webDriverFactoryProvider = value;
        DisposeCurrentWebDriver();
      }
    }

    /// <summary>
    /// Gets the default implementation of the <see cref="BrowseTheWeb"/> ability.
    /// </summary>
    /// <returns>The default web browsing ability.</returns>
    public static BrowseTheWeb GetDefaultWebBrowsingAbility()
    {
      var driver = GetWebDriver();
      return new BrowseTheWeb(driver, UriTransformer, true);
    }

    /// <summary>
    /// Disposes of the current web driver, if it exists.
    /// </summary>
    public static void DisposeCurrentWebDriver()
    {
      if(webDriver != null)
        webDriver.Dispose();
      webDriver = null;
    }

    static IWebDriver GetWebDriver()
    {
      if(webDriver == null)
      {
        var webdriverFactory = WebDriverFactoryProvider.GetFactory();
        webDriver = webdriverFactory.GetWebDriver();
      }

      return webDriver;
    }

    /// <summary>
    /// Initializes the <see cref="Stage"/> class.
    /// </summary>
    static Stage()
    {
      reporter = new NoOpReporter();
      cast = new Cast();
      uriTransformer = new NoOpUriTransformer();
      webDriver = null;
      webDriverFactoryProvider = new ConfigurationWebDriverFactoryProvider();
    }
  }
}
