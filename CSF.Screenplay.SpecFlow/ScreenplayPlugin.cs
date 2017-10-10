using System;
using System.Reflection;
using TechTalk.SpecFlow.Plugins;

[assembly: RuntimePlugin(typeof(CSF.Screenplay.SpecFlow.ScreenplayPlugin))]

namespace CSF.Screenplay.SpecFlow
{
  public class ScreenplayPlugin : IRuntimePlugin
  {
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
