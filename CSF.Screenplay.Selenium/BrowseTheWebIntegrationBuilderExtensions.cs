using System;
using CSF.FlexDi;
using CSF.Screenplay.Integration;
using CSF.Screenplay.Selenium.Abilities;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium
{
  /// <summary>
  /// Extension methods relating to the registration of the <see cref="BrowseTheWeb"/> ability for Screenplay.
  /// </summary>
  public static class BrowseTheWebIntegrationBuilderExtensions
  {

    /// <summary>
    /// Registers the ability to use a web browser with Screenplay.  A fresh ability instance will be created with each
    /// scenario throughout the test run.
    /// </summary>
    /// <param name="helper">Helper.</param>
    /// <param name="factory">Factory.</param>
    /// <param name="name">Name.</param>
    public static void UseWebBrowser(this IIntegrationConfigBuilder helper,
                                     Func<IResolvesServices,BrowseTheWeb> factory = null,
                                     string name = null)
    {
      if(helper == null)
        throw new ArgumentNullException(nameof(helper));

      var fac = factory?? CreateWebBrowser;

      helper.ServiceRegistrations.PerScenario.Add(h => {
        h.RegisterFactory(fac).AsOwnType().WithName(name);
      });
    }

    static BrowseTheWeb CreateWebBrowser(IResolvesServices resolver)
    {
      if(resolver == null)
        throw new ArgumentNullException(nameof(resolver));

      var driver = resolver.Resolve<IWebDriver>();
      var transformer = resolver.TryResolve<IUriTransformer>() ?? NoOpUriTransformer.Default;

      return new BrowseTheWeb(driver, transformer);
    }
  }
}
