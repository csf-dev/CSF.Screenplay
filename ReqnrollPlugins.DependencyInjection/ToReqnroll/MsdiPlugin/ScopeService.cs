using Microsoft.Extensions.DependencyInjection;
using Reqnroll.BoDi;

namespace ReqnrollPlugins.DependencyInjection.ToReqnroll.MsdiPlugin;

public abstract class ScopeService<TService>(DependencyLifetime lifetime, ReqnrollServiceHandle<IObjectContainer> reqnrollContainerHandle) where TService : notnull
{
    public TService Instance => reqnrollContainerHandle.Instance.Resolve<IServiceProvider>(lifetime.GetName()).GetRequiredService<TService>();
}

public class FeatureScopeService<TService>(ReqnrollServiceHandle<IObjectContainer> reqnrollContainerHandle) 
    : ScopeService<TService>(DependencyLifetime.Feature, reqnrollContainerHandle) where TService : notnull;

public class WorkerScopeService<TService>(ReqnrollServiceHandle<IObjectContainer> reqnrollContainerHandle) 
    : ScopeService<TService>(DependencyLifetime.Worker, reqnrollContainerHandle) where TService : notnull;
