using CalculatorApp;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll.BoDi;
using ReqnrollPlugins.DependencyInjection.StepDefinitions;
using ReqnrollPlugins.DependencyInjection.ToReqnroll.MsdiPlugin;

namespace ReqnrollPlugins.DependencyInjection.Support;

[DependencyConfiguration]
public class SetupTestDependencies : MsdiDependencyConfiguration
{
    protected override void SetupServices(ServiceCollection serviceCollection, IObjectContainer testRunContainer)
    {
        serviceCollection.AddSingleton<ICalculatorConfiguration, TestCalculatorConfiguration>();
        serviceCollection.AddScoped<ICalculator, Calculator>();

        // SAMPLE REGISTRATIONS

        // register test run dependencies:
        serviceCollection.AddSingleton<ITestRunDependency, TestRunDependency>();

        // register worker dependencies (use it from other scopes by depending on WorkerScopeService<IWorkerDependency>):
        serviceCollection.AddScoped<IWorkerDependency, WorkerDependency>();
        serviceCollection.AddTransient<WorkerScopeService<IWorkerDependency>>();

        // register feature dependencies (use it from other scopes by depending on WorkerScopeService<IFeatureDependency>):
        serviceCollection.AddScoped<IFeatureDependency, FeatureDependency>();
        serviceCollection.AddTransient<FeatureScopeService<IFeatureDependency>>();

        // register scenario dependencies:
        serviceCollection.AddScoped<IScenarioDependency, ScenarioDependency>();
    }

    // EARLIER CONFIGURATION:

    /*[ScenarioDependencies]
    public static IServiceCollection CreateServices()
    {
        var services = new ServiceCollection();

        services.AddSingleton<ICalculatorConfiguration, TestCalculatorConfiguration>();
        services.AddScoped<ICalculator, Calculator>();

        return services;
    }*/
}
