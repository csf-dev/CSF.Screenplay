using System;
using CSF.Screenplay.Context;
using CSF.Screenplay.Web.Abilities;
using CSF.WebDriverFactory;
using OpenQA.Selenium;

namespace CSF.Screenplay
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
    /// <param name="ability">Ability.</param>
    /// <param name="name">An optional identifying name for the ability instance.</param>
    public static void RegisterSingletonBrowseTheWebAbility(this IScreenplayContext context,
                                                            BrowseTheWeb ability,
                                                            string name = null)
    {
      if(context == null)
        throw new ArgumentNullException(nameof(context));
      if(ability == null)
        throw new ArgumentNullException(nameof(ability));

      context.RegisterSingleton(ability, name);
    }

    /// <summary>
    /// Registers the web browsing ability with the context.  This ability is used for all test cases/scenarios
    /// in the test run.
    /// </summary>
    /// <param name="context">Context.</param>
    /// <param name="driver">The Selenium WebDriver.</param>
    /// <param name="uriTransformer">An optional URI transformer.</param>
    /// <param name="name">An optional identifying name for the ability instance.</param>
    public static void RegisterSingletonBrowseTheWebAbility(this IScreenplayContext context,
                                                            IWebDriver driver,
                                                            IUriTransformer uriTransformer = null,
                                                            string name = null)
    {
      if(context == null)
        throw new ArgumentNullException(nameof(context));
      if(driver == null)
        throw new ArgumentNullException(nameof(driver));

      var ability = new BrowseTheWeb(driver, uriTransformer, true);

      context.RegisterSingleton(ability, name);
    }

    /// <summary>
    /// Gets the previously-registered web-browsing ability.
    /// </summary>
    /// <returns>The web browsing ability.</returns>
    /// <param name="context">Context.</param>
    /// <param name="name">An optional identifying name for the ability instance.</param>
    public static BrowseTheWeb GetWebBrowsingAbility(this IScreenplayContext context, string name = null)
    {
      if(context == null)
        throw new ArgumentNullException(nameof(context));

      return context.GetService<BrowseTheWeb>(name);
    }

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
