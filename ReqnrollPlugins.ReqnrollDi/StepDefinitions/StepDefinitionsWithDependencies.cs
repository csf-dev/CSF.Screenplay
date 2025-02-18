namespace ReqnrollPlugins.ReqnrollDi.StepDefinitions;

[Binding]
public class StepDefinitionsWithDependencies(
    IReqnrollOutputHelper outputHelper,
    IScenarioDependency scenarioDependency,
    IFeatureDependency featureDependency,
    IWorkerDependency workerDependency,
    ITestRunDependency testRunDependency)
{
    [When("a scenario dependency is used")]
    public void WhenAScenarioDependencyIsUsed()
    {
        outputHelper.WriteLine($"Scenario dependency used {scenarioDependency.Use()} times");
    }

    [When("a feature dependency is used")]
    public void WhenAFeatureDependencyIsUsed()
    {
        outputHelper.WriteLine($"Feature dependency used {featureDependency.Use()} times");
    }

    [When("a worker dependency is used")]
    public void WhenAWorkerDependencyIsUsed()
    {
        outputHelper.WriteLine($"Worker dependency used {workerDependency.Use()} times");
    }

    [When("a test run dependency is used")]
    public void WhenATestRunDependencyIsUsed()
    {
        outputHelper.WriteLine($"Test Run dependency used {testRunDependency.Use()} times");
    }

    [AfterTestRun]
    public static void AfterTestRun()
    {
        AppDomain.CurrentDomain.ProcessExit += (_, _) =>
        {
            File.AppendAllLines(@"..\..\..\log.txt", [$"{DateTime.Now}: Active objects at process exit (should be 0): {DependencyTester.ObjectCounter}"]);
        };
    }
}
