using Reqnroll.BoDi;

namespace ReqnrollPlugins.DependencyInjection.ToReqnroll.MsdiPlugin.Wrappers;

internal class ScenarioContextWrapper(IScenarioContext originalContext) : IScenarioContext
{
    public Exception TestError => originalContext.TestError;
    public ScenarioInfo ScenarioInfo => originalContext.ScenarioInfo;
    public ScenarioBlock CurrentScenarioBlock => originalContext.CurrentScenarioBlock;
    public IObjectContainer ScenarioContainer => originalContext.ScenarioContainer;
    public ScenarioExecutionStatus ScenarioExecutionStatus => originalContext.ScenarioExecutionStatus;
}