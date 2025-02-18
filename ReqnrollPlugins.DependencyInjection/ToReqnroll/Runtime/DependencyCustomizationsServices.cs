using System.Reflection;
using Reqnroll.BoDi;

namespace Reqnroll;


public static class DependencyCustomizationsServices
{
    public static IDependencyConfiguration GetDependencyConfiguration(Assembly assembly)
    {
        if (!TryGetDependencyConfiguration(assembly, out var dependencyConfiguration))
            throw new InvalidOperationException("No dependency configuration found in assembly");

        return dependencyConfiguration;
    }

    public static bool TryGetDependencyConfiguration(Assembly assembly, out IDependencyConfiguration dependencyConfiguration)
    {
        var configType = assembly.GetTypes()
            .Select(t => (Type: t, Attribute: t.GetCustomAttribute<DependencyConfigurationAttribute>()))
            .Where(t => t.Attribute != null)
            .OrderByDescending(t => t.Attribute!.Priority)
            .Select(t => t.Type)
            .FirstOrDefault();

        if (configType == null)
        {
            dependencyConfiguration = null!;
            return false;
        }

        var instance = Activator.CreateInstance(configType)!;
        if (instance is not IDependencyConfiguration configurationInstance)
            throw new InvalidOperationException($"Dependency configuration class {configType.FullName} must implement {nameof(IDependencyConfiguration)}");

        dependencyConfiguration = configurationInstance;
        return true;
    }

    public static Assembly[] GetBindingAssemblies(IObjectContainer testRunContainer)
    {
        return testRunContainer.Resolve<ITestRunnerManager>().BindingAssemblies;
    }

    public static IEnumerable<Type> GetBindingTypes(Assembly[] bindingAssemblies)
    {
        foreach (var assembly in bindingAssemblies)
        {
            foreach (var type in assembly.GetTypes().Where(t => Attribute.IsDefined(t, typeof(BindingAttribute))))
            {
                yield return type;
            }
        }
    }
}