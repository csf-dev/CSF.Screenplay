using Reqnroll.Bindings;
using Reqnroll.Infrastructure;

namespace Reqnroll;

public abstract class DependencyConfigurationOptions
{
    public bool AutoRegisterBindingTypes { get; set; } = true;

    public Dictionary<DependencyLifetime, List<Type>> ExposedReqnrollServices = new()
    {
        {
            DependencyLifetime.TestRun,
            [
                typeof(ITestRunContext)
            ]
        },
        {
            DependencyLifetime.Worker,
            [
                typeof(ITestThreadContext),
                typeof(TestThreadContext),
                typeof(IReqnrollOutputHelper),
                typeof(IContextManager),
                typeof(ITestRunner),
                typeof(ITestExecutionEngine), //do we need to expose this?
                typeof(IStepArgumentTypeConverter), //do we need to expose this?
                typeof(IStepDefinitionMatchService), //do we need to expose this?
            ]
        },
        { DependencyLifetime.Feature,
            [
                typeof(IFeatureContext),
                typeof(FeatureContext)
            ]
        },
        { DependencyLifetime.Scenario,
            [
                typeof(IScenarioContext),
                typeof(ScenarioContext)
            ]
        },
    };
}