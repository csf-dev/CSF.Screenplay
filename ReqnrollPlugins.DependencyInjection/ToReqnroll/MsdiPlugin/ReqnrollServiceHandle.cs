namespace ReqnrollPlugins.DependencyInjection.ToReqnroll.MsdiPlugin;

public class ReqnrollServiceHandle<TService>(TService instance)
{
    public TService Instance => instance;
}