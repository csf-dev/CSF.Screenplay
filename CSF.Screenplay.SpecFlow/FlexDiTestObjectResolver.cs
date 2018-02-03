using System;
using BoDi;
using TechTalk.SpecFlow.Infrastructure;

namespace CSF.Screenplay.SpecFlow
{
  public class FlexDiTestObjectResolver : ITestObjectResolver
  {
    public object ResolveBindingInstance(Type bindingType, IObjectContainer scenarioContainer)
    {
      var flexDiContainer = scenarioContainer.Resolve<FlexDi.IContainer>();
      return flexDiContainer.Resolve(bindingType);
    }
  }
}
