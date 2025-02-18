using Reqnroll.BoDi;

namespace ReqnrollPlugins.DependencyInjection.ToReqnroll.MsdiPlugin.Wrappers;

internal class ReqnrollContextAccessor
{
    private IObjectContainer? _container;

    public IObjectContainer Container
    {
        get => _container ?? throw new InvalidOperationException("The Reqnroll context is not initialized");
        set => _container = value;
    }
}