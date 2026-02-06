using System;
using Reqnroll.BoDi;
using ReqnrollPlugins.ReqnrollDi.ToReqnroll.Runtime.BoDi;

namespace CSF.Screenplay;

[DependencyConfiguration]
public class DependencyInjectionSetup : ReqnrollDiDependencyConfiguration
{
    protected override void SetupScenarioScope(IObjectContainer scenarioContainer)
    {
        Console.WriteLine("Yep");
    }
}