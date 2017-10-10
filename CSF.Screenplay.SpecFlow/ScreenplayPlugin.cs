using System;
using System.Reflection;
using TechTalk.SpecFlow.Plugins;

[assembly: RuntimePlugin(typeof(CSF.Screenplay.SpecFlow.ScreenplayPlugin))]

namespace CSF.Screenplay.SpecFlow
{
  /// <summary>
  /// SpecFlow plugin type for Screenplay
  /// </summary>
  public class ScreenplayPlugin : IRuntimePlugin
  {
    /// <summary>
    /// The entry-point for an <see cref="IRuntimePlugin"/>.  This initialises the plugin to provide Screenplay
    /// capabilities to the SpecFlow tests.
    /// </summary>
    /// <param name="runtimePluginEvents">Runtime plugin events.</param>
    /// <param name="runtimePluginParameters">Runtime plugin parameters.</param>
    public void Initialize(RuntimePluginEvents runtimePluginEvents,
                           RuntimePluginParameters runtimePluginParameters)
    {
      runtimePluginEvents.ConfigurationDefaults += (sender, e) => {
        e.SpecFlowConfiguration.AdditionalStepAssemblies.Add(ThisAssembly.FullName);
      };
    }

    Assembly ThisAssembly => Assembly.GetExecutingAssembly();
  }
}
