using Autofac;
using Reqnroll.BoDi;
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;
using Autofac.Core;
using System.Linq;
using Autofac.Extensions.DependencyInjection;

namespace ReqnrollPlugins.Autofac.ToReqnroll.AutofacPlugin;

public abstract class AutofacDependencyConfiguration(AutofacDependencyConfigurationOptions options) : IDependencyConfiguration
{
    protected AutofacDependencyConfiguration() : this(new AutofacDependencyConfigurationOptions())
    {
    }

    public AutofacDependencyConfigurationOptions Options { get; } = options;

    #region Customization methods for end users

    /// <summary>
    /// Allows to customize the container before it is built.
    /// </summary>
    /// <param name="containerBuilder">The container builder for the registrations.</param>
    /// <param name="testRunContainer">The Reqnroll test-run container</param>
    protected abstract void SetupServices(ContainerBuilder containerBuilder, IObjectContainer testRunContainer);

    /// <summary>
    /// Allows to customize the container for the scenario scope.
    /// </summary>
    /// <param name="scenarioContainerBuilder">The container builder for the scenario scope registrations.</param>
    /// <param name="scenarioContainer">The Reqnroll scenario container</param>
    protected virtual void SetupScenarioScope(ContainerBuilder scenarioContainerBuilder, IObjectContainer scenarioContainer)
    {
        //nop
    }

    /// <summary>
    /// Allows to customize the container for the feature scope.
    /// </summary>
    /// <param name="featureContainerBuilder">The container builder for the feature scope registrations.</param>
    /// <param name="featureContainer">The Reqnroll feature container</param>
    protected virtual void SetupFeatureScope(ContainerBuilder featureContainerBuilder, IObjectContainer featureContainer)
    {
        //nop
    }

    /// <summary>
    /// Allows to customize the container for the worker scope.
    /// </summary>
    /// <param name="workerContainerBuilder">The container builder for the worker scope registrations.</param>
    /// <param name="workerContainer">The Reqnroll worker container</param>
    protected virtual void SetupWorkerScope(ContainerBuilder workerContainerBuilder, IObjectContainer workerContainer)
    {
        //nop
    }

    /// <summary>
    /// Creates a new instance of the container builder. Can be overridden to provide a custom implementation.
    /// </summary>
    protected virtual ContainerBuilder CreateContainerBuilderInstance()
    {
        return new ContainerBuilder();
    }

    /// <summary>
    /// Allows any initialization of the scope for the scenario.
    /// </summary>
    protected virtual void InitializeScenarioScope(ILifetimeScope lifetimeScope, IObjectContainer scenarioContainer)
    {
        //nop
    }

    /// <summary>
    /// Allows any initialization of the scope for the feature.
    /// </summary>
    protected virtual void InitializeFeatureScope(ILifetimeScope lifetimeScope, IObjectContainer featureContainer)
    {
        //nop
    }

    /// <summary>
    /// Allows any initialization of the scope for the worker.
    /// </summary>
    protected virtual void InitializeWorkerScope(ILifetimeScope lifetimeScope, IObjectContainer workerContainer)
    {
        //nop
    }


    /// <summary>
    /// Allows any initialization of the scope for the test run.
    /// </summary>
    protected virtual void InitializeTestRunScope(ILifetimeScope lifetimeScope, IObjectContainer testRunContainer)
    {
        //nop
    }

    #endregion

    Exception IDependencyConfiguration.GetResolveError(Type targetType, string? missingTypeName, DependencyLifetime lifetime, IObjectContainer reqnrollContainer, Exception? innerException)
    {
        string? GetMissingTypeNameFromException(Exception? exception)
        {
            if (exception is not DependencyResolutionException || string.IsNullOrEmpty(exception.Message))
                return null;
            var match = Regex.Match(exception.Message, @"Cannot resolve parameter '(?<typeName>\S*)");
            if (!match.Success)
                return null;
            return match.Groups["typeName"].Value;
        }

        missingTypeName ??= GetMissingTypeNameFromException(innerException) ?? "interface-type";
        var instructions = $"To configure this service for {lifetime} scope, please " +
           $"add 'containerBuilder.RegisterType<your-implementation-type>().As<{missingTypeName}>().{lifetime}Scope()'" +
           $" to the {nameof(SetupServices)} method of your dependency configuration.";
        return new InvalidOperationException($"Could not resolve {targetType} for {lifetime} with Autofac. {instructions}", innerException);
    }

    bool IDependencyConfiguration.TryResolve(Type targetType, DependencyLifetime lifetime, IObjectContainer reqnrollContainer, out object instance)
    {
        var lifetimeScope = GetLifetimeScope(lifetime, reqnrollContainer);
        return lifetimeScope.TryResolve(targetType, out instance!);
    }

    private ILifetimeScope GetLifetimeScope(DependencyLifetime lifetime, IObjectContainer reqnrollContainer)
    {
        if (reqnrollContainer.TryResolveIfExplicitlyRegistered<ILifetimeScope>(out var lifetimeScope, lifetime.GetName()))
            return lifetimeScope;

        lock (reqnrollContainer)
        {
            if (reqnrollContainer.TryResolveIfExplicitlyRegistered(out lifetimeScope, lifetime.GetName()))
                return lifetimeScope!;

            lifetimeScope = CreateLifetimeScope(lifetime, reqnrollContainer);

            // We register the Autofac lifetime scope to the Reqnroll container to be able to resolve binding objects from it
            // The disposal of the Autofac scope is managed through the AutofacScopeDisposer registrations, hence dispose: false
            reqnrollContainer.RegisterInstanceAs(lifetimeScope, lifetime.GetName(), dispose: false);
            return lifetimeScope;
        }
    }

    private ILifetimeScope CreateLifetimeScope(DependencyLifetime lifetime, IObjectContainer reqnrollContainer)
    {
        ILifetimeScope CreateRootScope()
        {
            var containerBuilder = CreateRootContainerBuilder(reqnrollContainer);
            // build the root container
            var container = containerBuilder.Build(Options.ContainerBuildOptions);
            // register the root container so that it is disposed when the Reqnroll execution finishes
            reqnrollContainer.RegisterInstanceAs(new AutofacScopeDisposer(container), DependencyLifetime.TestRun.GetName(), dispose: true);
            return container;
        }

        ILifetimeScope CreateNestedScope()
        {
            var parentServiceProvider = GetLifetimeScope(lifetime - 1, reqnrollContainer.GetBaseContainer());
            var scope = parentServiceProvider.BeginLifetimeScope(lifetime.GetName(), 
                builder => SetupScopeContainerBuilder(builder, lifetime, reqnrollContainer));
            // register the Autofac lifetime scope so that it is disposed when the Reqnroll scope is disposed
            reqnrollContainer.RegisterInstanceAs(new AutofacScopeDisposer(scope), lifetime.GetName(), dispose: true);
            return scope;
        }

        var lifetimeScope = lifetime == DependencyLifetime.TestRun ?
            CreateRootScope() :
            CreateNestedScope();

        // let the user initialize the scope
        switch (lifetime)
        {
            case DependencyLifetime.Scenario:
                InitializeScenarioScope(lifetimeScope, reqnrollContainer);
                break;
            case DependencyLifetime.Feature:
                InitializeFeatureScope(lifetimeScope, reqnrollContainer);
                break;
            case DependencyLifetime.Worker:
                InitializeWorkerScope(lifetimeScope, reqnrollContainer);
                break;
            case DependencyLifetime.TestRun:
                InitializeTestRunScope(lifetimeScope, reqnrollContainer);
                break;
       }

        return lifetimeScope;
    }
    
    private ContainerBuilder CreateRootContainerBuilder(IObjectContainer testRunContainer)
    {
        var containerBuilder = CreateContainerBuilderInstance();

        SetupServices(containerBuilder, testRunContainer);
        containerBuilder.RegisterType<AutofacServiceProvider>().AsSelf().AsImplementedInterfaces();

        if (Options.AutoRegisterBindingTypes)
            RegisterBindingTypes(containerBuilder, DependencyCustomizationsServices.GetBindingAssemblies(testRunContainer));

        // register exposed Reqnroll services, like IScenarioContext
        RegisterReqnrollServices(containerBuilder);

        // make IObjectContainer of test run available both as named and as default dependency
        containerBuilder.RegisterInstance(testRunContainer)
                        .Named<IObjectContainer>(DependencyLifetime.TestRun.GetName())
                        .As<IObjectContainer>()
                        .ExternallyOwned();

        return containerBuilder;
    }

    private void SetupScopeContainerBuilder(ContainerBuilder containerBuilder, DependencyLifetime lifetime, IObjectContainer reqnrollContainer)
    {
        // make IObjectContainer of test run available both as named and as default dependency
        containerBuilder.RegisterInstance(reqnrollContainer)
                        .Named<IObjectContainer>(lifetime.GetName())
                        .As<IObjectContainer>()
                        .ExternallyOwned();

        switch (lifetime)
        {
            case DependencyLifetime.Scenario:
                SetupScenarioScope(containerBuilder, reqnrollContainer);
                break;
            case DependencyLifetime.Feature:
                SetupFeatureScope(containerBuilder, reqnrollContainer);
                break;
            case DependencyLifetime.Worker:
                SetupWorkerScope(containerBuilder, reqnrollContainer);
                break;
        }
    }

    private void RegisterBindingTypes(ContainerBuilder containerBuilder, Assembly[] bindingAssemblies)
    {
        foreach (var type in DependencyCustomizationsServices.GetBindingTypes(bindingAssemblies))
        {
            containerBuilder.RegisterType(type).ScenarioScope();
        }
    }

    private void RegisterReqnrollServices(ContainerBuilder containerBuilder)
    {
        object ResolveService(IComponentContext serviceProvider, DependencyLifetime lifetime, Type serviceType)
            => serviceProvider.ResolveNamed<IObjectContainer>(lifetime.GetName()).Resolve(serviceType);

        var exposedReqnrollServices = Options.ExposedReqnrollServices.SelectMany(entry => entry.Value.Select(t => (Lifetime: entry.Key, Type: t)));
        foreach (var service in exposedReqnrollServices)
        {
            containerBuilder
                .Register(ctx => ResolveService(ctx, service.Lifetime, service.Type))
                .As(service.Type)
                .InstancePerReqnrollScope(service.Lifetime)
                .ExternallyOwned();
        }
    }


    #region Helper classes

    /// <summary>
    /// Tracks the disposal of the Autofac scopes when the Reqnroll containers disposed.
    /// </summary>
    class AutofacScopeDisposer(ILifetimeScope scope) : IDisposable
    {
        private bool _isDisposed;

        public void Dispose()
        {
            if (_isDisposed)
            {
                Debug.WriteLine($"The scope {scope} has been disposed already.");
                return;
            }
            _isDisposed = true;
            scope.Dispose();
        }
    }
    #endregion
}