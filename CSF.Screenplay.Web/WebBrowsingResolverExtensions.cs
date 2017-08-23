using System;
using CSF.Screenplay.Scenarios;
using CSF.Screenplay.Web.Abilities;
using OpenQA.Selenium;

namespace CSF.Screenplay
{
  /// <summary>
  /// Extension methods related to the resolving of web-browsing-related services.
  /// </summary>
  public static class WebBrowsingResolverExtensions
  {
    /// <summary>
    /// Gets the web browsing ability from the current resolver.
    /// </summary>
    /// <returns>The web browser.</returns>
    /// <param name="resolver">Resolver.</param>
    /// <param name="name">Name.</param>
    public static BrowseTheWeb GetWebBrowser(this IServiceResolver resolver, string name = null)
    {
      if(resolver == null)
        throw new ArgumentNullException(nameof(resolver));

      return resolver.GetService<BrowseTheWeb>(name);
    }

    /// <summary>
    /// Configures a registered webdriver to be disposed after the <see cref="IScreenplayScenario.EndScenario"/>
    /// event is triggered. This provides cleanup functionality for per-scenario web drivers.
    /// </summary>
    /// <param name="scenario">Scenario.</param>
    /// <param name="webDriverName">Web driver name.</param>
    public static void DisposeWebDriverAfterScenario(this IScreenplayScenario scenario,
                                                     string webDriverName = null)
    {
      if(scenario == null)
        throw new ArgumentNullException(nameof(scenario));

      scenario.EndScenario += (sender, e) => {
        var driver = scenario.GetOptionalService<IWebDriver>(webDriverName);
        if(driver == null)
          return;

        driver.Dispose();
      };
    }
  }
}
