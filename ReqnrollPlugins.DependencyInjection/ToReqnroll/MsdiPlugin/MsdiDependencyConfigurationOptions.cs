using Microsoft.Extensions.DependencyInjection;

namespace ReqnrollPlugins.DependencyInjection.ToReqnroll.MsdiPlugin;

public class MsdiDependencyConfigurationOptions : DependencyConfigurationOptions
{
    public ServiceProviderOptions ServiceProviderOptions { get; set; } = new ServiceProviderOptions();
}