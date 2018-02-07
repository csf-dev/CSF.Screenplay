using System;
using CSF.FlexDi;
using CSF.Screenplay.Integration;
using CSF.Screenplay.Selenium.Abilities;

namespace CSF.Screenplay.Selenium
{
  /// <summary>
  /// Extension methods relating to the registration of URI transformers for Screenplay.
  /// </summary>
  public static class UriTransformerIntegrationBuilderExtensions
  {

    /// <summary>
    /// Registers a URI transformer into Screenplay. The transformer will be created afresh for every scenario
    /// in the test run.
    /// </summary>
    /// <param name="helper">Helper.</param>
    /// <param name="factory">Factory.</param>
    /// <param name="name">Name.</param>
    public static void UseUriTransformer(this IIntegrationConfigBuilder helper,
                                         Func<IResolvesServices,IUriTransformer> factory,
                                         string name = null)
    {
      if(helper == null)
        throw new ArgumentNullException(nameof(helper));
      if(factory == null)
        throw new ArgumentNullException(nameof(factory));

      helper.ServiceRegistrations.PerScenario.Add(b => {
        b.RegisterDynamicFactory(factory).AsOwnType().WithName(name);
      });
    }

    /// <summary>
    /// Registers a URI transformer into Screenplay. The single transformer instance will be shared/reused throughout
    /// all scenarios in the test run.
    /// </summary>
    /// <param name="helper">Helper.</param>
    /// <param name="transformer">Transformer.</param>
    /// <param name="name">Name.</param>
    public static void UseSharedUriTransformer(this IIntegrationConfigBuilder helper,
                                               IUriTransformer transformer,
                                               string name = null)
    {
      if(helper == null)
        throw new ArgumentNullException(nameof(helper));
      if(transformer == null)
        throw new ArgumentNullException(nameof(transformer));

      helper.ServiceRegistrations.PerTestRun.Add(b => {
        b.RegisterInstance(transformer).As<IUriTransformer>().WithName(name);
      });
    }
  }
}
