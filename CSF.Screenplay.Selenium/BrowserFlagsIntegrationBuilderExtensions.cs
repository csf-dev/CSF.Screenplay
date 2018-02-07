using System;
using CSF.FlexDi;
using CSF.Screenplay.Integration;
using CSF.WebDriverExtras.Flags;

namespace CSF.Screenplay.Selenium
{
  public static class BrowserFlagsIntegrationBuilderExtensions
  {
    public static void UseBrowserFlags(this IIntegrationConfigBuilder helper,
                                       params string[] extraFlagsDefinitionFilePaths)
    {
      if(helper == null)
        throw new ArgumentNullException(nameof(helper));

      helper.ServiceRegistrations.PerTestRun.Add(h => {
        h.RegisterFactory(() => new FlagsDefinitionProvider(extraFlagsDefinitionFilePaths))
         .As<IProvidesFlagsDefinitions>();

        h.RegisterFactory((IProvidesFlagsDefinitions provider) => {
          var definitions = provider.GetFlagsDefinitions();
          return new BrowserFlagsProvider(definitions);
        })
         .As<IGetsBrowserFlags>();
      });
    }

    public static void UseBrowserFlags(this IIntegrationConfigBuilder helper,
                                       IGetsBrowserFlags flagsProvider)
    {
      if(helper == null)
        throw new ArgumentNullException(nameof(helper));

      helper.ServiceRegistrations.PerTestRun.Add(h => {
        h.RegisterInstance(flagsProvider).AsOwnType();
      });
    }

    public static void UseBrowserFlags(this IIntegrationConfigBuilder helper,
                                       Func<IResolvesServices,IGetsBrowserFlags> flagsProviderFactory)
    {
      if(helper == null)
        throw new ArgumentNullException(nameof(helper));

      helper.ServiceRegistrations.PerScenario.Add(h => {
        h.RegisterDynamicFactory(flagsProviderFactory).AsOwnType();
      });
    }
  }
}
