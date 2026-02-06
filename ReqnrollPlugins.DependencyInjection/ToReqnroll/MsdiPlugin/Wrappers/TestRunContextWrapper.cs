using Reqnroll.BoDi;

namespace ReqnrollPlugins.DependencyInjection.ToReqnroll.MsdiPlugin.Wrappers;

internal class TestRunContextWrapper(ITestRunContext originalContext) : ITestRunContext
{
    public Exception TestError => originalContext.TestError;

    public IObjectContainer TestRunContainer => originalContext.TestRunContainer;

    public string TestDirectory => originalContext.TestDirectory;
}