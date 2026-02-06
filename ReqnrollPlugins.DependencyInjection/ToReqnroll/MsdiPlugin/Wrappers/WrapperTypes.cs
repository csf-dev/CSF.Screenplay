namespace ReqnrollPlugins.DependencyInjection.ToReqnroll.MsdiPlugin.Wrappers;

internal static class WrapperTypes
{
    private static readonly Dictionary<Type, (Type WrapperType, Type[] SuggestedFor)> Types = new()
    {
        { typeof(IScenarioContext), (typeof(ScenarioContextWrapper), [typeof(ScenarioContext)]) },
        { typeof(IFeatureContext), (typeof(FeatureContextWrapper),[typeof(FeatureContext)])},
        { typeof(ITestThreadContext), (typeof(TestThreadContextWrapper),[typeof(TestThreadContext)])},
        { typeof(ITestRunContext), (typeof(TestRunContextWrapper),[])},
    };

    public static bool TryGetValue(Type serviceType, out Type wrapperType)
    {
        if (!Types.TryGetValue(serviceType, out (Type WrapperType, Type[] SuggestedFor) value))
        {
            wrapperType = null!;
            return false;
        }

        wrapperType = value.WrapperType;
        return true;
    }

    public static Type? GetSuggestedType(Type serviceType)
    {
        return Types
                .Where(entry => entry.Value.SuggestedFor.Contains(serviceType))
                .Select(entry => entry.Key)
                .FirstOrDefault();
    }
}