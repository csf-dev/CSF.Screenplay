using System;
using System.Collections.Concurrent;
using System.Reflection;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performances;
using Microsoft.Extensions.DependencyInjection;

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
    /// The Screenplay plugin for Reqnroll.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This plugin class is the <xref href="IntegrationGlossaryItem?text=test+framework+integration"/> for Reqnroll.
    /// Crucially it adds the Screenplay architecture to the Reqnroll architecture.
    /// </para>
    /// <para>
    /// Becuase this plugin leverages the Reqnroll/BoDi <c>IObjectContainer</c>, it is likely incompatible with other plugins
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
    /// When using Reqnroll's default BoDi container this is described in the following article
    /// <see href="https://docs.reqnroll.net/latest/automation/context-injection.html#advanced-options"/>.
    /// If using a third-party DI container then you should use that container's appropriate mechanism of adding services.
    /// </para>
    /// </remarks>
    public class ScreenplayPlugin : IRuntimePlugin
    {
        readonly object syncRoot = new object();

        bool initialised;

        /// <summary>
        /// Provides static access to the Screenplay instance.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is required because the bindings for beginning/ending the Screenplay in <see cref="ScreenplayBinding"/> must be <c>static</c>.
        /// Those bindings use the <c>[BeforeTestRun]</c> and <c>[AfterTestRun]</c> hooks, which must be static, as documented here:
        /// <see href="https://docs.reqnroll.net/latest/automation/hooks.html#supported-hook-attributes"/>
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
        /// It was a known/documented issue that this event may be triggered more than once in a single run of the old
        /// SpecFlow, and by more than one thread.  Unfortunately I don't know if that's still applicable in Reqnroll.
        /// I've opened this discussion to see if I can find out: <see href="https://github.com/orgs/reqnroll/discussions/1005"/>.
        /// To prevent double-initialisation, this method occurs in a thread-safe manner which ensures that even if it
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

                var boDiContainer = args.ObjectContainer;
                var services = new ServiceCollectionAdapter(boDiContainer);
                services.AddScreenplayPlugin();
                boDiContainer.RegisterFactoryAs(c => c.ToServiceProvider());
                Screenplay = boDiContainer.Resolve<Screenplay>();
                initialised = true;
            }
        }

        static void OnCustomizeScenarioDependencies(object sender, CustomizeScenarioDependenciesEventArgs args)
        {
            var boDiContainer = args.ObjectContainer;
            var services = boDiContainer.ToServiceProvider();
            boDiContainer.RegisterInstanceAs(services);

            var serviceCollection = boDiContainer.ToServiceCollection();
            serviceCollection
                .AddSingleton(s => s.GetRequiredService<PerformanceProviderFactory>().GetPerformanceContainer(s))
                .AddSingleton(s => s.GetRequiredService<PerformanceProvider>().GetCurrentPerformance())
                .AddSingleton<ICast>(s => new Cast(s, s.GetRequiredService<IPerformance>().PerformanceIdentity))
                .AddSingleton<IStage, Stage>();
        }

    }
}
