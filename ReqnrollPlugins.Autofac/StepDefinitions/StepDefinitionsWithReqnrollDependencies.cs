namespace ReqnrollPlugins.Autofac.StepDefinitions;

[Binding]
public class StepDefinitionsWithReqnrollDependencies(
    IReqnrollOutputHelper outputHelper,

    IScenarioContext scenarioContext,
    ScenarioContext scenarioContextHandle,

    IFeatureContext featureContext,
    FeatureContext featureContextHandle,

    ITestThreadContext testThreadContext,
    TestThreadContext testThreadContextHandle,
    ITestRunner testRunner,

    ITestRunContext testRunContext,
    ITestRunContext testRunContextHandle
)
{
    [When("a Reqnroll scenario dependency is used")]
    public void GivenAReqnrollScenarioDependencyIsUsed()
    {
        outputHelper.WriteLine("Scenario dependency used (1): " + scenarioContext.ScenarioInfo.Title);
        outputHelper.WriteLine("Scenario dependency used (2): " + scenarioContextHandle.ScenarioInfo.Title);
    }

    [When("a Reqnroll feature dependency is used")]
    public void GivenAReqnrollFeatureDependencyIsUsed()
    {
        outputHelper.WriteLine("Feature dependency used (1): " + featureContext.FeatureInfo.Title);
        outputHelper.WriteLine("Feature dependency used (2): " + featureContextHandle.FeatureInfo.Title);
    }

    [When("a Reqnroll worker dependency is used")]
    public void GivenAReqnrollWorkerDependencyIsUsed()
    {
        outputHelper.WriteLine("Worker dependency used (1): " + testThreadContext.TestError);
        outputHelper.WriteLine("Worker dependency used (2): " + testThreadContextHandle.TestError);
        outputHelper.WriteLine("Worker dependency used (3): " + testRunner.TestWorkerId);
    }

    [When("a Reqnroll test run dependency is used")]
    public void GivenAReqnrollTestRunDependencyIsUsed()
    {
        outputHelper.WriteLine("Test Run dependency used (1): " + testRunContext.TestDirectory);
        outputHelper.WriteLine("Test Run dependency used (2): " + testRunContextHandle.TestDirectory);
    }
}