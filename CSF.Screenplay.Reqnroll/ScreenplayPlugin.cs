using System;
using System.Collections.Concurrent;
using System.Reflection;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performances;
#if SPECFLOW
using BoDi;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Plugins;
using TechTalk.SpecFlow.UnitTestProvider;
#else
using Reqnroll.BoDi;
using Reqnroll;
using Reqnroll.Plugins;
using Reqnroll.UnitTestProvider;
#endif

namespace CSF.Screenplay
{
    /// <summary>
    /// The Screenplay plugin for SpecFlow.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This plugin class is the <xref href="IntegrationGlossaryItem?text=test+framework+integration"/> for SpecFlow.
    /// Crucially it adds the Screenplay architecture to the SpecFlow architecture.
    /// </para>
    /// <para>
    /// Becuase this plugin leverages the SpecFlow/BoDi <c>IObjectContainer</c>, it is likely incompatible with other plugins
    /// which integrate with third party Dependency Injection libraries.
    /// </para>
    /// <para>
    /// This may be easily worked-around, though.  If you are using a third-party DI plugin then do not use this plugin.
    /// Instead use the <see cref="ScreenplayServiceCollectionExtensions.AddScreenplay(Microsoft.Extensions.DependencyInjection.IServiceCollection, Action{ScreenplayOptions})"/>
    /// method to add Screenplay to that third-party DI system, when customising the dependency registrations.
    /// Adding Screenplay in that way is equivalent to the work done by this plugin.
    /// </para>
    /// <para>
    /// If you wish to further customise the dependency injection, such as adding injectable services for <xref href="AbilityGlossaryItem?text=abilities"/>
    /// or implementations of <see cref="IPersona"/>, add them to the relevant DI container.
    /// When using SpecFlow's default BoDi container this is described in the following article
    /// <see href="https://docs.specflow.org/projects/specflow/en/latest/Bindings/Context-Injection.html#advanced-options"/>.
    /// If using a third-party DI container then you should use that container's appropriate mechanism of adding services.
    /// </para>
    /// </remarks>
    public class ScreenplayPlugin : IRuntimePlugin
    {
        readonly object syncRoot = new object();
        readonly ConcurrentDictionary<FeatureInfo, Guid> featureContextIds = new ConcurrentDictionary<FeatureInfo, Guid>();
        readonly ConcurrentDictionary<ScenarioAndFeatureInfoKey, Guid> scenarioContextIds = new ConcurrentDictionary<ScenarioAndFeatureInfoKey, Guid>();

        bool initialised;

        /// <summary>
        /// Provides static access to the Screenplay instance.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is required because the bindings for beginning/ending the Screenplay in <see cref="ScreenplayBinding"/> must be <c>static</c>:
        /// <see href="https://docs.specflow.org/projects/specflow/en/latest/Bindings/Hooks.html#supported-hook-attributes"/>.
        /// </para>
        /// </remarks>
        static public Screenplay Screenplay { get; private set; }

        /// <inheritdoc/>
        public void Initialize(RuntimePluginEvents runtimePluginEvents,
                               RuntimePluginParameters runtimePluginParameters,
                               UnitTestProviderConfiguration unitTestProviderConfiguration)
        {
            runtimePluginEvents.CustomizeGlobalDependencies += OnCustomizeGlobalDependencies;
            runtimePluginEvents.CustomizeScenarioDependencies += OnCustomizeScenarioDependencies;
            runtimePluginEvents.ConfigurationDefaults += OnConfigurationDefaults;
        }

        static void OnConfigurationDefaults(object sender, ConfigurationDefaultsEventArgs e)
        {
            var config = 
#if SPECFLOW
                e.SpecFlowConfiguration;
#else
                e.ReqnrollConfiguration;
#endif
            config.AdditionalStepAssemblies.Add(Assembly.GetExecutingAssembly().FullName);
        }

        /// <summary>
        /// Event handler for the <c>CustomizeGlobalDependencies</c> runtime plugin event.
        /// </summary>
        /// <remarks>
        /// <para>
        /// It is a known/documented issue that this event may be triggered more than once in a single run of SpecFlow:
        /// <see href="https://github.com/techtalk/SpecFlow/issues/948"/>, and by more than one thread.
        /// Thus, to prevent double-initialisation, this method occurs in a thread-safe manner which ensures that even if it
        /// is executed more than once, there is no adverse consequence.
        /// </para>
        /// </remarks>
        /// <param name="sender">The event sender</param>
        /// <param name="args">Event args to customize the global dependencies</param>
        void OnCustomizeGlobalDependencies(object sender, CustomizeGlobalDependenciesEventArgs args)
        {
            lock(syncRoot)
            {
                if (initialised) return;

                var container = args.ObjectContainer;
                var serviceCollection = new ServiceCollectionAdapter(container);
                serviceCollection.AddScreenplay();
                container.RegisterFactoryAs<IServiceProvider>(c => new ServiceProviderAdapter(c));
                Screenplay = container.Resolve<Screenplay>();
                initialised = true;
            }
        }

        void OnCustomizeScenarioDependencies(object sender, CustomizeScenarioDependenciesEventArgs args)
        {
            var container = args.ObjectContainer;
            var services = new ServiceProviderAdapter(container);
            container.RegisterInstanceAs<IServiceProvider>(services);
            var performance = new Performance(services, new [] { GetFeatureIdAndName(container), GetScenarioIdAndName(container) });
            var performanceContainer = new PerformanceProvider();
            performanceContainer.SetCurrentPerformance(performance);
            container.RegisterInstanceAs(performanceContainer);
            container.RegisterFactoryAs(c => c.Resolve<PerformanceProvider>().GetCurrentPerformance());

            container.RegisterFactoryAs<ICast>(c => new Cast(c.Resolve<IServiceProvider>(), c.Resolve<IPerformance>().PerformanceIdentity));
            container.RegisterTypeAs<Stage, IStage>();
        }

        IdentifierAndName GetFeatureIdAndName(IObjectContainer container)
        {
            var featureInfo = container.Resolve<FeatureInfo>();
            return new IdentifierAndName(GetFeatureId(featureInfo).ToString(),
                                         featureInfo.Title,
                                         true);
        }

        Guid GetFeatureId(FeatureInfo featureContext) => featureContextIds.GetOrAdd(featureContext, _ => Guid.NewGuid());

        IdentifierAndName GetScenarioIdAndName(IObjectContainer container)
        {
            var featureInfo = container.Resolve<FeatureInfo>();
            var scenarioInfo = container.Resolve<ScenarioInfo>();
            return new IdentifierAndName(GetScenarioId(featureInfo, scenarioInfo).ToString(),
                                         scenarioInfo.Title,
                                         true);
        }

        Guid GetScenarioId(FeatureInfo featureInfo, ScenarioInfo scenarioInfo)
            => scenarioContextIds.GetOrAdd(new ScenarioAndFeatureInfoKey(scenarioInfo, featureInfo), _ => Guid.NewGuid());
    }
}
