using System;
using CSF.FlexDi;
using CSF.Screenplay.Integration;
using CSF.WebDriverExtras.Flags;

namespace CSF.Screenplay.Selenium
{
  /// <summary>
  /// Extension methods relating to the registration of an implementation <see cref="IGetsBrowserFlags"/> for Screenplay.
  /// </summary>
  public static class BrowserFlagsIntegrationBuilderExtensions
  {
    /// <summary>
    /// Indicates that browser flags are to be taken from the hard-coded default flags, as well as from a collection of
    /// file paths containing JSON-formatted definitions.
    /// </summary>
    /// <param name="helper">Helper.</param>
    /// <param name="extraFlagsDefinitionFilePaths">Extra flags definition file paths.</param>
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

    /// <summary>
    /// Uses a given implementation of <see cref="IGetsBrowserFlags"/> for all tests.
    /// </summary>
    /// <param name="helper">Helper.</param>
    /// <param name="flagsProvider">Flags provider.</param>
    public static void UseBrowserFlags(this IIntegrationConfigBuilder helper,
                                       IGetsBrowserFlags flagsProvider)
    {
      if(helper == null)
        throw new ArgumentNullException(nameof(helper));

      helper.ServiceRegistrations.PerTestRun.Add(h => {
        h.RegisterInstance(flagsProvider).AsOwnType();
      });
    }

    /// <summary>
    /// Uses a factory per scenario to get an implementation of <see cref="IGetsBrowserFlags"/>.
    /// </summary>
    /// <param name="helper">Helper.</param>
    /// <param name="flagsProviderFactory">Flags provider factory.</param>
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
