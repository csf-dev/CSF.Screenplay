using Reqnroll.BoDi;

namespace ReqnrollPlugins.DependencyInjection.ToReqnroll.MsdiPlugin.Wrappers;

internal class TestThreadContextWrapper(ITestThreadContext originalContext) : ITestThreadContext
{
    public Exception TestError => originalContext.TestError;

    public IObjectContainer TestThreadContainer => originalContext.TestThreadContainer;
}