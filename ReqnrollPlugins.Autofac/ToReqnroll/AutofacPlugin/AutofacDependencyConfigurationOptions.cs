using Autofac.Builder;

namespace ReqnrollPlugins.Autofac.ToReqnroll.AutofacPlugin;

public class AutofacDependencyConfigurationOptions : DependencyConfigurationOptions
{
    public ContainerBuildOptions ContainerBuildOptions { get; set; } = ContainerBuildOptions.None;
}