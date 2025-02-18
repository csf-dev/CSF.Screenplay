using ReqnrollPlugins.DependencyInjection.ToReqnroll.MsdiPlugin;

namespace ReqnrollPlugins.DependencyInjection.StepDefinitions
{
    [Binding]
    public class StepDefinitionsWithReqnrollDependencies(
        IReqnrollOutputHelper outputHelper,

        IScenarioContext scenarioContext,
        ReqnrollServiceHandle<ScenarioContext> scenarioContextHandle,

        IFeatureContext featureContext,
        ReqnrollServiceHandle<FeatureContext> featureContextHandle,

        ITestThreadContext testThreadContext,
        ReqnrollServiceHandle<TestThreadContext> testThreadContextHandle,
        ITestRunner testRunner,

        ITestRunContext testRunContext,
        ReqnrollServiceHandle<ITestRunContext> testRunContextHandle
        )
    {
        [When("a Reqnroll scenario dependency is used")]
        public void GivenAReqnrollScenarioDependencyIsUsed()
        {
            outputHelper.WriteLine("Scenario dependency used (1): " + scenarioContext.ScenarioInfo.Title);
            outputHelper.WriteLine("Scenario dependency used (2): " + scenarioContextHandle.Instance.ScenarioInfo.Title);
        }

        [When("a Reqnroll feature dependency is used")]
        public void GivenAReqnrollFeatureDependencyIsUsed()
        {
            outputHelper.WriteLine("Feature dependency used (1): " + featureContext.FeatureInfo.Title);
            outputHelper.WriteLine("Feature dependency used (2): " + featureContextHandle.Instance.FeatureInfo.Title);
        }

        [When("a Reqnroll worker dependency is used")]
        public void GivenAReqnrollWorkerDependencyIsUsed()
        {
            outputHelper.WriteLine("Worker dependency used (1): " + testThreadContext.TestError);
            outputHelper.WriteLine("Worker dependency used (2): " + testThreadContextHandle.Instance.TestError);
            outputHelper.WriteLine("Worker dependency used (3): " + testRunner.TestWorkerId);
        }

        [When("a Reqnroll test run dependency is used")]
        public void GivenAReqnrollTestRunDependencyIsUsed()
        {
            outputHelper.WriteLine("Test Run dependency used (1): " + testRunContext.TestDirectory);
            outputHelper.WriteLine("Test Run dependency used (2): " + testRunContextHandle.Instance.TestDirectory);
        }
    }
}
