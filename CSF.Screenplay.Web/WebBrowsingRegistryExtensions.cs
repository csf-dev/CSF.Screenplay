using System;
using CSF.Screenplay.Scenarios;
using CSF.Screenplay.Web.Abilities;
using OpenQA.Selenium;

namespace CSF.Screenplay
{
  public static class WebBrowsingRegistryExtensions
  {
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

    public static void RegisterUriTransformer(this IServiceRegistryBuilder builder,
                                              Func<IServiceResolver,IUriTransformer> factory = null,
                                              string name = null)
    {
      if(builder == null)
        throw new ArgumentNullException(nameof(builder));

      if(factory != null)
        builder.RegisterPerScenario(factory, name);

      builder.RegisterPerScenario(res => NoOpUriTransformer.Default, name);
    }

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
