//
// ScreenplayDependencyInjectionBuilder.cs
//
// Author:
//       Craig Fowler <craig@csf-dev.com>
//
// Copyright (c) 2018 Craig Fowler
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using BoDi;
using TechTalk.SpecFlow;

namespace CSF.Screenplay.SpecFlow
{
  /// <summary>
  /// A builder which propagates the SpecFlow standard dependencies into the FlexDi container.
  /// </summary>
  public class ScreenplayDependencyInjectionBuilder
  {
    readonly IObjectContainer specflowContainer;
    readonly FlexDi.IContainer screenplayContainer;

    /// <summary>
    /// Re-registers the SpecFlow standard dependencies into the FlexDi container.
    /// </summary>
    public void ReRegisterSpecFlowDependencies()
    {
      RegisterSpecFlowDefaultDependencies();
      ConfigureContainerDependentObjects();
    }

    void RegisterSpecFlowDefaultDependencies()
    {
      /* Taken from a combination of:
       *    TechTalk.SpecFlow.Infrastructure.DefaultDependencyProvider
       *    TechTalk.SpecFlow.Infrastructure.ContainerBuilder
       */
      TryRegisterInstance<FeatureInfo>();
      TryRegisterInstance<ScenarioInfo>();
      TryRegisterInstance<TechTalk.SpecFlow.Configuration.IRuntimeConfigurationProvider>();
      TryRegisterInstance<TechTalk.SpecFlow.Plugins.RuntimePluginEvents>();
      TryRegisterInstance<TechTalk.SpecFlow.Configuration.SpecFlowConfiguration>();
      TryRegisterInstance<TechTalk.SpecFlow.UnitTestProvider.IUnitTestRuntimeProvider>();

      TryRegisterInstance<TechTalk.SpecFlow.Configuration.IRuntimeConfigurationProvider>();
      TryRegisterInstance<ITestRunnerManager>();
      TryRegisterInstance<TechTalk.SpecFlow.Tracing.IStepFormatter>();
      TryRegisterInstance<TechTalk.SpecFlow.Tracing.ITestTracer>();
      TryRegisterInstance<TechTalk.SpecFlow.Tracing.ITraceListener>();
      TryRegisterInstance<TechTalk.SpecFlow.Tracing.ITraceListenerQueue>();
      TryRegisterInstance<TechTalk.SpecFlow.ErrorHandling.IErrorProvider>();
      TryRegisterInstance<TechTalk.SpecFlow.Bindings.Discovery.IRuntimeBindingSourceProcessor>();
      TryRegisterInstance<TechTalk.SpecFlow.Bindings.Discovery.IRuntimeBindingRegistryBuilder>();
      TryRegisterInstance<TechTalk.SpecFlow.Bindings.IBindingRegistry>();
      TryRegisterInstance<TechTalk.SpecFlow.Bindings.IBindingFactory>();
      TryRegisterInstance<TechTalk.SpecFlow.Bindings.IStepDefinitionRegexCalculator>();
      TryRegisterInstance<TechTalk.SpecFlow.Bindings.IBindingInvoker>();
      TryRegisterInstance<TechTalk.SpecFlow.Infrastructure.ITestObjectResolver>();
      TryRegisterInstance<TechTalk.SpecFlow.BindingSkeletons.IStepDefinitionSkeletonProvider>();
      TryRegisterInstance<TechTalk.SpecFlow.BindingSkeletons.ISkeletonTemplateProvider>();
      TryRegisterInstance<TechTalk.SpecFlow.BindingSkeletons.IStepTextAnalyzer>();
      TryRegisterInstance<TechTalk.SpecFlow.Plugins.IRuntimePluginLoader>();
      TryRegisterInstance<TechTalk.SpecFlow.Infrastructure.IBindingAssemblyLoader>();
      TryRegisterInstance<TechTalk.SpecFlow.Configuration.IConfigurationLoader>();

      TryRegisterInstance<ITestRunner>();
      TryRegisterInstance<TechTalk.SpecFlow.Infrastructure.IContextManager>();
      TryRegisterInstance<TechTalk.SpecFlow.Infrastructure.ITestExecutionEngine>();
      TryRegisterInstance<TechTalk.SpecFlow.Bindings.IStepArgumentTypeConverter>();
      TryRegisterInstance<TechTalk.SpecFlow.Infrastructure.IStepDefinitionMatchService>();
    }

    void TryRegisterInstance<T>() where T : class
    {
      Func<IObjectContainer,T> accessor = c => c.Resolve<T>();
      try
      {
        var instance = accessor(specflowContainer);
        screenplayContainer.AddRegistrations(b => b.RegisterInstance(instance));
      }
      catch(ObjectContainerException) { }
    }

    IObjectContainer CreateObjectContainerProxy()
    {
      return new BoDiContainerProxy(screenplayContainer);
    }

    void ConfigureContainerDependentObjects()
    {
      var containerProxy = CreateObjectContainerProxy();
      screenplayContainer.AddRegistrations(r => r.RegisterInstance(containerProxy).As<IObjectContainer>());
      containerProxy.ObjectCreated += obj => {
        var containerDependentObject = obj as TechTalk.SpecFlow.Infrastructure.IContainerDependentObject;
        if(containerDependentObject == null) return;

        containerDependentObject.SetObjectContainer(containerProxy);
      };
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.SpecFlow.ScreenplayDependencyInjectionBuilder"/> class.
    /// </summary>
    /// <param name="specflowContainer">Specflow container.</param>
    /// <param name="screenplayContainer">Screenplay container.</param>
    public ScreenplayDependencyInjectionBuilder(IObjectContainer specflowContainer,
                                                FlexDi.IContainer screenplayContainer)
    {
      if(screenplayContainer == null)
        throw new ArgumentNullException(nameof(screenplayContainer));
      if(specflowContainer == null)
        throw new ArgumentNullException(nameof(specflowContainer));

      this.screenplayContainer = screenplayContainer;
      this.specflowContainer = specflowContainer;
    }
  }
}
