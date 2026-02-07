using System;
using Autofac;
using Reqnroll.BoDi;
using ReqnrollPlugins.Autofac.ToReqnroll.AutofacPlugin;

namespace CSF.Screenplay;

[DependencyConfiguration]
public class DependencyInjectionSetup : AutofacDependencyConfiguration
{
    protected override void SetupServices(ContainerBuilder containerBuilder, IObjectContainer testRunContainer)
    {
        Console.WriteLine("Yep");
    }
}