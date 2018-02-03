using System;
using System.Reflection;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Bindings;
using TechTalk.SpecFlow.Infrastructure;
using TechTalk.SpecFlow.Plugins;

[assembly: RuntimePlugin(typeof(CSF.Screenplay.SpecFlow.ScreenplayPlugin))]

namespace CSF.Screenplay.SpecFlow
{
  /// <summary>
  /// SpecFlow plugin type for Screenplay
  /// </summary>
  public class ScreenplayPlugin : IRuntimePlugin
  {
    readonly object syncRoot = new object();

    /// <summary>
    /// The entry-point for an <see cref="IRuntimePlugin"/>.  This initialises the plugin to provide Screenplay
    /// capabilities to the SpecFlow tests.
    /// </summary>
    /// <param name="runtimePluginEvents">Runtime plugin events.</param>
    /// <param name="runtimePluginParameters">Runtime plugin parameters.</param>
    public void Initialize(RuntimePluginEvents runtimePluginEvents,
                           RuntimePluginParameters runtimePluginParameters)
    {
      AddPluginStepsAssemblies(runtimePluginEvents);
      ConfigureFlexDiDependencyInjection(runtimePluginEvents);
    }

    void AddPluginStepsAssemblies(RuntimePluginEvents runtimePluginEvents)
    {
      runtimePluginEvents.ConfigurationDefaults += (sender, e) => {
        e.SpecFlowConfiguration.AdditionalStepAssemblies.Add(ThisAssembly.FullName);
      };
    }

    void ConfigureFlexDiDependencyInjection(RuntimePluginEvents runtimePluginEvents)
    {
      runtimePluginEvents.CustomizeGlobalDependencies += (sender, e) => {
        lock(syncRoot)
        {
          // Apparently this can end up being called multiple times: https://github.com/techtalk/SpecFlow/issues/948
          // So, I'm protecting it from performing registration more than once
          if(e.ObjectContainer.IsRegistered<FlexDiTestObjectResolver>())
            return;

          e.ObjectContainer.RegisterTypeAs<FlexDiTestObjectResolver,ITestObjectResolver>();
        }
      };

      runtimePluginEvents.CustomizeScenarioDependencies += (sender, e) => {
        var container = e.ObjectContainer;
        var scenarioContext = container.Resolve<ScenarioContext>();
        var featureContext = container.Resolve<FeatureContext>();

        var adapter = ScreenplayBinding.GetScenarioAdapter(scenarioContext, featureContext);
        var scenario = adapter.CreateScenario();

        container.RegisterInstanceAs(scenario.DiContainer);

        scenario.DiContainer.AddRegistrations(r => {
          r.RegisterInstance(scenarioContext);
          r.RegisterInstance(featureContext);
          r.RegisterInstance(scenario).As<IScenario>();
        });
      };
    }

    Assembly ThisAssembly => Assembly.GetExecutingAssembly();
  }
}
