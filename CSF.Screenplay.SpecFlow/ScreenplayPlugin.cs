using System;
using System.Threading;
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
        readonly ReaderWriterLockSlim syncRoot = new ReaderWriterLockSlim();
        bool initialised;

        /// <inheritdoc/>
        public void Initialize(RuntimePluginEvents runtimePluginEvents,
                               RuntimePluginParameters runtimePluginParameters,
                               UnitTestProviderConfiguration unitTestProviderConfiguration)
        {
            runtimePluginEvents.CustomizeGlobalDependencies += OnCustomizeGlobalDependencies;
            throw new NotImplementedException();
        }

        void OnCustomizeGlobalDependencies(object sender, CustomizeGlobalDependenciesEventArgs args)
        {
            try
            {
                syncRoot.EnterUpgradeableReadLock();
                if (initialised) return;

                syncRoot.EnterWriteLock();
                var serviceCollection = new ServiceCollectionAdapter(args.ObjectContainer);
                serviceCollection.AddScreenplay();
                args.ObjectContainer.RegisterFactoryAs<IServiceProvider>(container => new ServiceProviderAdapter(container));
                initialised = true;
            }
            finally
            {
                if (syncRoot.IsWriteLockHeld)
                    syncRoot.ExitWriteLock();
                if (syncRoot.IsUpgradeableReadLockHeld)
                    syncRoot.ExitUpgradeableReadLock();
            }
        }
    }
}