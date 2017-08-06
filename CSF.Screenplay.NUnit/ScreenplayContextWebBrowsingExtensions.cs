using System;
using CSF.Screenplay.Context;
using CSF.Screenplay.Web.Abilities;
using CSF.WebDriverFactory;
using OpenQA.Selenium;

namespace CSF.Screenplay.NUnit
{
  /// <summary>
  /// Extension methods related to registering and retrieving the <see cref="BrowseTheWeb"/> ability
  /// associated with the current context.
  /// </summary>
  public static class ScreenplayContextWebBrowsingExtensions
  {
    /// <summary>
    /// Registers the web browsing ability with the context.  This ability is used for all test cases/scenarios
    /// in the test run.
    /// </summary>
    /// <param name="context">Context.</param>
    /// <param name="uriTransformer">An optional URI transformer.</param>
    /// <param name="name">An optional identifying name for the ability instance.</param>
    public static void RegisterSingletonBrowseTheWebAbility(this IScreenplayContext context,
                                                            IUriTransformer uriTransformer = null,
                                                            string name = null)
    {
      if(context == null)
        throw new ArgumentNullException(nameof(context));

      var driver = GetDriver();

      context.RegisterSingletonBrowseTheWebAbility(driver, uriTransformer, name);
    }

    static IWebDriver GetDriver()
    {
      var provider = new ConfigurationWebDriverFactoryProvider();
      var factory = provider.GetFactory();
      return factory.GetWebDriver();
    }
  }
}
