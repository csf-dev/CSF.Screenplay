using System;
using System.Collections.Concurrent;
using System.Reflection;
using BoDi;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performances;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Plugins;
using TechTalk.SpecFlow.UnitTestProvider;

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
    /// Instead use the <see cref="ScreenplayServiceCollectionExtensions.AddScreenplay(Microsoft.Extensions.DependencyInjection.IServiceCollection)"/>
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
        readonly ConcurrentDictionary<FeatureContext, Guid> featureContextIds = new ConcurrentDictionary<FeatureContext, Guid>();
        readonly ConcurrentDictionary<ScenarioAndFeatureContextKey, Guid> scenarioContextIds = new ConcurrentDictionary<ScenarioAndFeatureContextKey, Guid>();

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
            e.SpecFlowConfiguration.AdditionalStepAssemblies.Add(Assembly.GetExecutingAssembly().FullName);
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
            
            var performanceFactory = container.Resolve<ICreatesPerformance>();
            var performance = performanceFactory.CreatePerformance();
            performance.NamingHierarchy.Add(GetFeatureIdAndName(container));
            performance.NamingHierarchy.Add(GetScenarioIdAndName(container));

            container.RegisterInstanceAs(performance);
            container.RegisterFactoryAs<ICast>(c => new Cast(c.Resolve<IServiceProvider>(), c.Resolve<IPerformance>().PerformanceIdentity));
            container.RegisterTypeAs<Stage, IStage>();
        }

        IdentifierAndName GetFeatureIdAndName(IObjectContainer container)
        {
            var featureContext = container.Resolve<FeatureContext>();
            return new IdentifierAndName(GetFeatureId(featureContext).ToString(),
                                         featureContext.FeatureInfo.Title,
                                         true);
        }

        Guid GetFeatureId(FeatureContext featureContext) => featureContextIds.GetOrAdd(featureContext, _ => Guid.NewGuid());

        IdentifierAndName GetScenarioIdAndName(IObjectContainer container)
        {
            var featureContext = container.Resolve<FeatureContext>();
            var scenarioContext = container.Resolve<ScenarioContext>();
            return new IdentifierAndName(GetScenarioId(featureContext, scenarioContext).ToString(),
                                         scenarioContext.ScenarioInfo.Title,
                                         true);
        }

        Guid GetScenarioId(FeatureContext featureContext, ScenarioContext scenarioContext)
            => scenarioContextIds.GetOrAdd(new ScenarioAndFeatureContextKey(scenarioContext, featureContext), _ => Guid.NewGuid());
    }
}
