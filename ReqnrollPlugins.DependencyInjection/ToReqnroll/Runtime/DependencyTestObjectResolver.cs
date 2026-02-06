using Reqnroll.BoDi;
using Reqnroll.Infrastructure;

namespace Reqnroll;


public class DependencyTestObjectResolver : ITestObjectResolver
{
    private readonly IObjectContainer _testRunContainer;
    private readonly TestObjectResolver _defaultResolver;
    private readonly IDependencyConfiguration? _dependencyConfiguration;

    public DependencyTestObjectResolver(ITestAssemblyProvider testAssemblyProvider, IObjectContainer testRunContainer, TestObjectResolver defaultResolver)
    {
        _testRunContainer = testRunContainer;
        _defaultResolver = defaultResolver;
        if (!DependencyCustomizationsServices.TryGetDependencyConfiguration(testAssemblyProvider.TestAssembly, out _dependencyConfiguration))
            _dependencyConfiguration = null;
    }

    public object ResolveBindingInstance(Type bindingType, IObjectContainer container)
    {
        if (_dependencyConfiguration == null)
        {
            return _defaultResolver.ResolveBindingInstance(bindingType, container);
        }

        var lifetime = container switch
        {
            _ when container.IsRegistered(typeof(ScenarioContext)) => DependencyLifetime.Scenario,
            _ when container.IsRegistered(typeof(FeatureContext)) => DependencyLifetime.Feature,
            _ when container.IsRegistered(typeof(TestThreadContext)) => DependencyLifetime.Worker,
            _ when container == _testRunContainer => DependencyLifetime.TestRun,
            _ => throw new InvalidOperationException("Unable to classify object container")
        };

        try
        {
            if (_dependencyConfiguration.TryResolve(bindingType, lifetime, container, out var instance)) return instance;
        }
        catch (Exception ex)
        {
            throw _dependencyConfiguration.GetResolveError(bindingType, null, lifetime, container, ex);
        }

        if (container.TryResolveIfExplicitlyRegistered(bindingType, out var reqnrollInstance))
            return reqnrollInstance;

        throw _dependencyConfiguration.GetResolveError(bindingType, bindingType.FullName, lifetime, container, null);
    }
}