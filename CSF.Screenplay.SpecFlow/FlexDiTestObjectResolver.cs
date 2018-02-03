using System;
using BoDi;
using TechTalk.SpecFlow.Infrastructure;

namespace CSF.Screenplay.SpecFlow
{
  /// <summary>
  /// An implementation of <c>TechTalk.SpecFlow.Infrastructure.ITestObjectResolver</c> which makes use of a
  /// FlexDi container.
  /// </summary>
  public class FlexDiTestObjectResolver : ITestObjectResolver
  {
    /// <summary>
    /// Resolves (instantiates) an instance of a type which is decorated with the <c>TechTalk.SpecFlow.BindingAttribute</c>.
    /// </summary>
    /// <returns>The binding instance.</returns>
    /// <param name="bindingType">The type of the binding instance to resolve.</param>
    /// <param name="scenarioContainer">The BoDi container for the current SpecFlow scenario.</param>
    public object ResolveBindingInstance(Type bindingType, IObjectContainer scenarioContainer)
    {
      var flexDiContainer = scenarioContainer.Resolve<FlexDi.IContainer>();
      return flexDiContainer.Resolve(bindingType);
    }
  }
}
