using Microsoft.Extensions.DependencyInjection;
using Reqnroll.BoDi;
using ReqnrollPlugins.DependencyInjection.ToReqnroll.MsdiPlugin.Wrappers;
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ReqnrollPlugins.DependencyInjection.ToReqnroll.MsdiPlugin;

public abstract class MsdiDependencyConfiguration(MsdiDependencyConfigurationOptions options) : IDependencyConfiguration
{
    protected MsdiDependencyConfiguration() : this(new MsdiDependencyConfigurationOptions())
    {
    }

    public MsdiDependencyConfigurationOptions Options { get; } = options;

    #region Customization methods for end users

    /// <summary>
    /// Allows to customize the service collection before it is built.
    /// </summary>
    /// <param name="serviceCollection">The service collection for the registrations.</param>
    /// <param name="testRunContainer">The Reqnroll test-run container</param>
    protected abstract void SetupServices(ServiceCollection serviceCollection, IObjectContainer testRunContainer);

    /// <summary>
    /// Creates a new instance of the service collection. Can be overridden to provide a custom implementation.
    /// </summary>
    protected virtual ServiceCollection CreateServiceCollectionInstance()
    {
        return new ServiceCollection();
    }

    /// <summary>
    /// Allows any initialization of the scope for the scenario.
    /// </summary>
    protected virtual void InitializeScenarioScope(IServiceProvider serviceProvider, IObjectContainer scenarioContainer)
    {
        //nop
    }

    /// <summary>
    /// Allows any initialization of the scope for the feature.
    /// </summary>
    protected virtual void InitializeFeatureScope(IServiceProvider serviceProvider, IObjectContainer featureContainer)
    {
        //nop
    }

    /// <summary>
    /// Allows any initialization of the scope for the worker.
    /// </summary>
    protected virtual void InitializeWorkerScope(IServiceProvider serviceProvider, IObjectContainer workerContainer)
    {
        //nop
    }


    /// <summary>
    /// Allows any initialization of the scope for the test run.
    /// </summary>
    protected virtual void InitializeTestRunScope(IServiceProvider serviceProvider, IObjectContainer testRunContainer)
    {
        //nop
    }

    #endregion

    Exception IDependencyConfiguration.GetResolveError(Type targetType, string? missingTypeName, DependencyLifetime lifetime, IObjectContainer reqnrollContainer, Exception? innerException)
    {
        string? GetMissingTypeNameFromException(Exception? exception)
        {
            if (exception is not InvalidOperationException || string.IsNullOrEmpty(exception.Message))
                return null;
            var match = Regex.Match(exception.Message, @"Unable to resolve service for type '(?<typeName>\S*)");
            if (!match.Success)
                return null;
            return match.Groups["typeName"].Value;
        }

        missingTypeName ??= GetMissingTypeNameFromException(innerException) ?? "interface-type";
        var instructions = $"To configure this service for {lifetime} scope, please " +
            lifetime switch
            {
                DependencyLifetime.TestRun => $"add 'serviceCollection.AddSingleton<{missingTypeName}, your-implementation-type>()'",
                DependencyLifetime.Scenario => $"add 'serviceCollection.AddScoped<{missingTypeName}, your-implementation-type>()'",
                _ => $"add 'serviceCollection.AddScoped<{missingTypeName}, your-implementation-type>()' and 'serviceCollection.AddTransient<WorkerScopeService<{targetType.Name}>>()'"
            } +
            $" to the {nameof(SetupServices)} method of your dependency configuration.";
        return new InvalidOperationException($"Could not resolve {targetType} for {lifetime} with Microsoft.Extensions.DependencyInjection. {instructions}", innerException);
    }

    bool IDependencyConfiguration.TryResolve(Type targetType, DependencyLifetime lifetime, IObjectContainer reqnrollContainer, out object instance)
    {
        var serviceProvider = GetLifetimeScope(lifetime, reqnrollContainer);
        var instanceOrNull = serviceProvider.GetService(targetType);
        if (instanceOrNull == null)
        {
            instance = default!;
            return false;
        }
        instance = instanceOrNull;
        return true;
    }

    private IServiceProvider GetLifetimeScope(DependencyLifetime lifetime, IObjectContainer reqnrollContainer)
    {
        if (reqnrollContainer.TryResolveIfExplicitlyRegistered<IServiceProvider>(out var serviceProvider, lifetime.GetName()))
            return serviceProvider;

        lock (reqnrollContainer)
        {
            if (reqnrollContainer.TryResolveIfExplicitlyRegistered(out serviceProvider, lifetime.GetName()))
                return serviceProvider!;

            serviceProvider = CreateLifetimeScope(lifetime, reqnrollContainer);

            // We register the MSDI service provider to the Reqnroll container to be able to resolve binding objects from it
            // The disposal of the MSDI scope is managed through the MsdiScopeDisposer registrations, hence dispose: false
            reqnrollContainer.RegisterInstanceAs(serviceProvider, lifetime.GetName(), dispose: false);

            return serviceProvider;
        }
    }

    private IServiceProvider CreateLifetimeScope(DependencyLifetime lifetime, IObjectContainer reqnrollContainer)
    {
        IServiceProvider CreateRootScope()
        {
            var serviceCollection = CreateRootServiceCollection(reqnrollContainer);
            // build the root service provider
            var serviceProvider = serviceCollection.BuildServiceProvider(Options.ServiceProviderOptions);
            // register the root service provider so that it is disposed when the Reqnroll execution finishes
            reqnrollContainer.RegisterInstanceAs(new MsdiScopeDisposer(serviceProvider), DependencyLifetime.TestRun.GetName(), dispose: true);
            return serviceProvider;
        }

        IServiceProvider CreateNestedScope()
        {
            var parentServiceProvider = GetLifetimeScope(lifetime - 1, reqnrollContainer.GetBaseContainer());
            var scope = parentServiceProvider.CreateScope();
            // register the MSDI scope so that it is disposed when the Reqnroll scope is disposed
            reqnrollContainer.RegisterInstanceAs(new MsdiScopeDisposer(scope), lifetime.GetName(), dispose: true);
            return scope.ServiceProvider;
        }

        var serviceProvider = lifetime == DependencyLifetime.TestRun ?
            CreateRootScope() :
            CreateNestedScope();

        // ensure ambient access to the Reqnroll container
        serviceProvider.GetRequiredService<ReqnrollContextAccessor>().Container = reqnrollContainer;

        // let the user initialize the scope
        switch (lifetime)
        {
            case DependencyLifetime.Scenario:
                InitializeScenarioScope(serviceProvider, reqnrollContainer);
                break;
            case DependencyLifetime.Feature:
                InitializeFeatureScope(serviceProvider, reqnrollContainer);
                break;
            case DependencyLifetime.Worker:
                InitializeWorkerScope(serviceProvider, reqnrollContainer);
                break;
            case DependencyLifetime.TestRun:
                InitializeTestRunScope(serviceProvider, reqnrollContainer);
                break;
        }

        return serviceProvider;
    }

    private IServiceCollection CreateRootServiceCollection(IObjectContainer testRunContainer)
    {
        var serviceCollection = CreateServiceCollectionInstance();

        // let users setup dependencies
        SetupServices(serviceCollection, testRunContainer);

        if (Options.AutoRegisterBindingTypes)
            RegisterBindingTypes(serviceCollection, DependencyCustomizationsServices.GetBindingAssemblies(testRunContainer));

        // register the ambient Reqnroll context accessor
        serviceCollection.AddScoped<ReqnrollContextAccessor>();

        // register exposed Reqnroll services, like IScenarioContext
        RegisterReqnrollServices(serviceCollection);

        // make ReqnrollServiceHandle<IObjectContainer> available
        serviceCollection.AddTransient(sp => new ReqnrollServiceHandle<IObjectContainer>(sp.GetRequiredService<ReqnrollContextAccessor>().Container));

        return serviceCollection;
    }

    protected void RegisterBindingTypes(IServiceCollection serviceCollection, Assembly[] bindingAssemblies)
    {
        foreach (var type in DependencyCustomizationsServices.GetBindingTypes(bindingAssemblies))
        {
            serviceCollection.AddScoped(type);
        }
    }

    private void RegisterReqnrollServices(ServiceCollection serviceCollection)
    {
        var exposedReqnrollServices = Options.ExposedReqnrollServices.SelectMany(entry => entry.Value);
        foreach (var serviceType in exposedReqnrollServices)
        {
            RegisterReqnrollService(serviceType, serviceCollection);
        }
    }

    private void RegisterReqnrollService(Type serviceType, ServiceCollection serviceCollection)
    {
        var handleType = typeof(ReqnrollServiceHandle<>).MakeGenericType(serviceType);
        object CreateHandle(object service)
            => Activator.CreateInstance(handleType, service)!;

        object ResolveService(IServiceProvider serviceProvider)
            => serviceProvider.GetRequiredService<ReqnrollContextAccessor>().Container.Resolve(serviceType);

        serviceCollection.AddTransient(handleType, sp => CreateHandle(ResolveService(sp)));

        if (WrapperTypes.TryGetValue(serviceType, out var wrapperType))
        {
            object CreateWrapper(object service)
                => Activator.CreateInstance(wrapperType, service)!;

            serviceCollection.AddTransient(serviceType, sp => CreateWrapper(ResolveService(sp)));
            return;
        }

        serviceCollection.AddTransient(serviceType, sp =>
        {
            var service = ResolveService(sp);
            if (service is not IDisposable)
                return service;

            var suggestedType = WrapperTypes.GetSuggestedType(serviceType);
            if (suggestedType != null)
                throw new Exception($"Please use a dependency to {suggestedType} instead of {serviceType}.");

            throw new Exception(
                $"Please use a dependency to {handleType.Name.TrimEnd('`', '1')}<{serviceType.Name}> instead of {serviceType} directly");
        });
    }


    #region Helper classes
    /// <summary>
    /// Tracks the disposal of the MSDI scopes when the Reqnroll containers disposed.
    /// </summary>
    class MsdiScopeDisposer(IDisposable scope) : IDisposable
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

    /// <summary>
    /// Supports ambient access of the Reqnroll IObjectContainer of the current lifetime.
    /// </summary>
    class ReqnrollContextAccessor
    {
        private IObjectContainer? _container;

        public IObjectContainer Container
        {
            get => _container ?? throw new InvalidOperationException("The Reqnroll context is not initialized");
            set => _container = value;
        }
    }
    #endregion
}