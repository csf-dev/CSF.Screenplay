using System;
using CSF.Screenplay.Scenarios;
using CSF.Screenplay.Web.Abilities;
using OpenQA.Selenium;

namespace CSF.Screenplay
{
  /// <summary>
  /// Extension methods related to registering web-browsing-related components with the current registry builder.
  /// </summary>
  public static class WebBrowsingRegistryExtensions
  {
    /// <summary>
    /// Registers a factory which will create a web driver instance for each scenario in the test run.
    /// </summary>
    /// <param name="builder">Builder.</param>
    /// <param name="factory">Factory.</param>
    /// <param name="name">Name.</param>
    public static void RegisterWebDriver(this IServiceRegistryBuilder builder,
                                         Func<IServiceResolver,IWebDriver> factory,
                                         string name = null)
    {
      if(builder == null)
        throw new ArgumentNullException(nameof(builder));
      if(factory == null)
        throw new ArgumentNullException(nameof(factory));

      builder.RegisterPerScenario(factory, name);
    }

    /// <summary>
    /// Registers a single web driver instance which will be used for every scenario in the test run.
    /// </summary>
    /// <param name="builder">Builder.</param>
    /// <param name="driver">Driver.</param>
    /// <param name="name">Name.</param>
    public static void RegisterWebDriver(this IServiceRegistryBuilder builder,
                                         IWebDriver driver,
                                         string name = null)
    {
      if(builder == null)
        throw new ArgumentNullException(nameof(builder));
      if(driver == null)
        throw new ArgumentNullException(nameof(driver));

      builder.RegisterSingleton(driver, name);
    }

    /// <summary>
    /// Registers a transformer instance which will transform all URIs sent to the web browsing ability.
    /// The transformer will be created afresh for each scenario in the test run.
    /// </summary>
    /// <param name="builder">Builder.</param>
    /// <param name="factory">Factory.</param>
    /// <param name="name">Name.</param>
    public static void RegisterUriTransformer(this IServiceRegistryBuilder builder,
                                              Func<IServiceResolver,IUriTransformer> factory,
                                              string name = null)
    {
      if(builder == null)
        throw new ArgumentNullException(nameof(builder));
      if(factory == null)
        throw new ArgumentNullException(nameof(factory));
      
      builder.RegisterPerScenario(factory, name);
    }

    /// <summary>
    /// Registers a transformer instance which will transform all URIs sent to the web browsing ability.
    /// A single transformer will be used across all scenarios in the test run.
    /// </summary>
    /// <param name="builder">Builder.</param>
    /// <param name="transformer">Transformer.</param>
    /// <param name="name">Name.</param>
    public static void RegisterUriTransformer(this IServiceRegistryBuilder builder,
                                              IUriTransformer transformer,
                                              string name = null)
    {
      if(builder == null)
        throw new ArgumentNullException(nameof(builder));
      if(transformer == null)
        throw new ArgumentNullException(nameof(transformer));

      builder.RegisterSingleton(transformer, name);
    }

    /// <summary>
    /// Registers a web-browsing ability, which is created afresh for each scenario in the test run.
    /// </summary>
    /// <param name="builder">Builder.</param>
    /// <param name="factory">Factory.</param>
    /// <param name="name">Name.</param>
    public static void RegisterWebBrowser(this IServiceRegistryBuilder builder,
                                          Func<IServiceResolver,BrowseTheWeb> factory = null,
                                          string name = null)
    {
      if(builder == null)
        throw new ArgumentNullException(nameof(builder));

      if(factory != null)
        builder.RegisterPerScenario(factory, name);

      builder.RegisterPerScenario(CreateWebBrowser, name);
    }

    static BrowseTheWeb CreateWebBrowser(IServiceResolver resolver)
    {
      if(resolver == null)
        throw new ArgumentNullException(nameof(resolver));

      var driver = resolver.GetService<IWebDriver>();
      var transformer = resolver.GetOptionalService<IUriTransformer>();
      return new BrowseTheWeb(driver, transformer?? NoOpUriTransformer.Default);
    }
  }
}
