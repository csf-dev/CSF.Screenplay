using Autofac;
using CalculatorApp;
using Reqnroll.BoDi;
using ReqnrollPlugins.Autofac.StepDefinitions;
using ReqnrollPlugins.Autofac.ToReqnroll.AutofacPlugin;

namespace ReqnrollPlugins.Autofac.Support;

[DependencyConfiguration]
public class SetupTestDependencies: AutofacDependencyConfiguration
{
    protected override void SetupServices(ContainerBuilder containerBuilder, IObjectContainer testRunContainer)
    {
        // Register globally scoped runtime dependencies
        containerBuilder
            .RegisterType<TestCalculatorConfiguration>()
            .As<ICalculatorConfiguration>()
            .TestRunScope(); // == .SingleInstance();

        // Register scenario scoped runtime dependencies
        containerBuilder
            .RegisterType<Calculator>()
            .As<ICalculator>()
            .ScenarioScope(); // == .InstancePerMatchingLifetimeScope(DependencyLifetime.Scenario.GetName());


        // SAMPLE REGISTRATIONS

        // register test run dependencies:
        containerBuilder.RegisterType<TestRunDependency>()
            .As<ITestRunDependency>()
            .TestRunScope(); // == .SingleInstance();

        // register worker dependencies (use it from other scopes by depending on WorkerScopeService<IWorkerDependency>):
        containerBuilder.RegisterType<WorkerDependency>()
            .As<IWorkerDependency>()
            .WorkerScope(); // == .InstancePerMatchingLifetimeScope(DependencyLifetime.Worker.GetName());

        // register feature dependencies (use it from other scopes by depending on WorkerScopeService<IFeatureDependency>):
        containerBuilder.RegisterType<FeatureDependency>()
            .As<IFeatureDependency>()
            .FeatureScope(); // == .InstancePerMatchingLifetimeScope(DependencyLifetime.Feature.GetName());

        // register scenario dependencies:
        containerBuilder.RegisterType<ScenarioDependency>()
            .As<IScenarioDependency>()
            .ScenarioScope(); // == .InstancePerMatchingLifetimeScope(DependencyLifetime.Scenario.GetName());
    }

    // ALTERNATIVE STYLE to register dependencies for scenario, feature, and worker scopes
    protected override void SetupScenarioScope(ContainerBuilder scenarioContainerBuilder, IObjectContainer scenarioContainer)
    {
        // alternative way to register scenario scoped dependencies
        //scenarioContainerBuilder.RegisterType<ScenarioDependency>()
        //                        .As<IScenarioDependency>()
        //                        .SingleInstance();
    }

    // EARLIER CONFIGURATION:

    /*
    [GlobalDependencies]
    public static void SetupGlobalContainer(ContainerBuilder containerBuilder)
    {
        // Register globally scoped runtime dependencies
        containerBuilder
            .RegisterType<TestCalculatorConfiguration>()
            .As<ICalculatorConfiguration>()
            .SingleInstance();
    }

    [ScenarioDependencies]
    public static void SetupScenarioContainer(ContainerBuilder containerBuilder)
    {
        // Register scenario scoped runtime dependencies
        containerBuilder
            .RegisterType<Calculator>()
            .As<ICalculator>()
            .SingleInstance();

        // register binding classes
        containerBuilder.AddReqnrollBindings<SetupTestDependencies>();
    }*/
}
