using Reqnroll.Infrastructure;
using Reqnroll.Plugins;
using Reqnroll.UnitTestProvider;
using ReqnrollPlugins.DependencyInjection.ToReqnroll.Runtime;

[assembly: RuntimePlugin(typeof(SetupPlugin))]

namespace ReqnrollPlugins.DependencyInjection.ToReqnroll.Runtime;

public class SetupPlugin : IRuntimePlugin
{
    public void Initialize(RuntimePluginEvents runtimePluginEvents, RuntimePluginParameters runtimePluginParameters, UnitTestProviderConfiguration unitTestProviderConfiguration)
    {
        runtimePluginEvents.CustomizeGlobalDependencies += (_, args) =>
        {
            args.ObjectContainer.RegisterTypeAs<DependencyTestObjectResolver, ITestObjectResolver>();
        };

        // this should be integrated to Reqnroll anyway
        runtimePluginEvents.CustomizeScenarioDependencies += (_, args) =>
        {
            args.ObjectContainer.RegisterFactoryAs<IScenarioContext>(c => c.Resolve<ScenarioContext>());
        };

        runtimePluginEvents.CustomizeFeatureDependencies += (_, args) =>
        {
            args.ObjectContainer.RegisterFactoryAs<IFeatureContext>(c => c.Resolve<FeatureContext>());
        };

        runtimePluginEvents.CustomizeTestThreadDependencies += (_, args) =>
        {
            args.ObjectContainer.RegisterFactoryAs<ITestThreadContext>(c => c.Resolve<TestThreadContext>());
        };
    }
}