using Reqnroll.BoDi;

namespace Reqnroll;


public static class ObjectContainerExtensions
{
    public static bool TryResolveIfExplicitlyRegistered<TObject>(this IObjectContainer container, out TObject instance, string? name = null)
    {
        if (container.IsRegistered<TObject>(name))
        {
            instance = container.Resolve<TObject>(name);
            return true;
        }
        instance = default!;
        return false;
    }
    public static bool TryResolveIfExplicitlyRegistered(this IObjectContainer container, Type targetType, out object instance, string? name = null)
    {
        if (container.IsRegistered(targetType, name))
        {
            instance = container.Resolve(targetType, name);
            return true;
        }
        instance = default!;
        return false;
    }

    public static bool TryResolve(this IObjectContainer container, Type targetType, out object instance, string? name = null)
    {
        if (!container.IsRegistered(targetType, name) &&
            (targetType.IsPrimitive || targetType == typeof(string) || targetType.IsValueType || targetType.IsInterface))
        {
            instance = default!;
            return false;
        }

        instance = container.Resolve(targetType, name);
        return true;
    }

    public static IObjectContainer GetBaseContainer(this IObjectContainer objectContainer)
    {
        return ((ObjectContainer)objectContainer).BaseContainer;
    }


}