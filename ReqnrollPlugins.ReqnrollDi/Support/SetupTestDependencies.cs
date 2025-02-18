using CalculatorApp;
using Reqnroll.BoDi;
using ReqnrollPlugins.ReqnrollDi.ToReqnroll.Runtime.BoDi;
using ReqnrollPlugins.ReqnrollDi.StepDefinitions;

namespace ReqnrollPlugins.ReqnrollDi.Support;

[DependencyConfiguration]
public class SetupTestDependencies : ReqnrollDiDependencyConfiguration
{
    protected override void SetupScenarioScope(IObjectContainer scenarioContainer)
    {
        scenarioContainer.RegisterTypeAs<Calculator, ICalculator>();

        // SAMPLE REGISTRATION - SCENARIO
        scenarioContainer.RegisterTypeAs<ScenarioDependency, IScenarioDependency>();
    }

    protected override void SetupTestRunScope(IObjectContainer testRunContainer)
    {
        testRunContainer.RegisterTypeAs<TestCalculatorConfiguration, ICalculatorConfiguration>();

        // SAMPLE REGISTRATION - TEST RUN
        testRunContainer.RegisterTypeAs<TestRunDependency, ITestRunDependency>();
    }

    protected override void SetupFeatureScope(IObjectContainer featureContainer)
    {
        // SAMPLE REGISTRATION - FEATURE
        featureContainer.RegisterTypeAs<FeatureDependency, IFeatureDependency>();
    }

    protected override void SetupWorkerScope(IObjectContainer workerContainer)
    {
        // SAMPLE REGISTRATION - WORKER
        workerContainer.RegisterTypeAs<WorkerDependency, IWorkerDependency>();
    }
}
