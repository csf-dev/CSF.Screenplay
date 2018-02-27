//
// BoDiContainerProxy.cs
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
using System.Collections.Generic;
using BoDi;
using CSF.FlexDi.Registration;

namespace CSF.Screenplay.SpecFlow
{
  class BoDiContainerProxy : IObjectContainer
  {
    readonly FlexDi.IContainer proxiedContainer;

    public event Action<object> ObjectCreated;

    public void Dispose()
    {
      proxiedContainer.Dispose();
    }

    public bool IsRegistered<T>() => proxiedContainer.HasRegistration<T>();

    public bool IsRegistered<T>(string name) => proxiedContainer.HasRegistration<T>(name);

    public void RegisterFactoryAs<TInterface>(Func<IObjectContainer, TInterface> factoryDelegate, string name = null)
    {
      proxiedContainer.AddRegistrations(r => r.RegisterFactory(factoryDelegate, typeof(TInterface)).WithName(name));
    }

    public void RegisterInstanceAs(object instance, Type interfaceType, string name = null, bool dispose = false)
    {
      proxiedContainer.AddRegistrations(r => r.RegisterInstance(instance).As(interfaceType).WithName(name).DisposeWithContainer(dispose));
    }

    public void RegisterInstanceAs<TInterface>(TInterface instance, string name = null, bool dispose = false) where TInterface : class
    {
      proxiedContainer.AddRegistrations(r => r.RegisterInstance(instance).As<TInterface>().WithName(name).DisposeWithContainer(dispose));
    }

    public void RegisterTypeAs<TType, TInterface>(string name = null) where TType : class, TInterface
    {
      proxiedContainer.AddRegistrations(r => r.RegisterType(typeof(TType)).As(typeof(TInterface)).WithName(name));
    }

    public object Resolve(Type typeToResolve, string name = null) => proxiedContainer.Resolve(typeToResolve, name);

    public T Resolve<T>() => proxiedContainer.Resolve<T>();

    public T Resolve<T>(string name) => proxiedContainer.Resolve<T>(name);

    public IEnumerable<T> ResolveAll<T>() where T : class => proxiedContainer.ResolveAll<T>();

    void InvokeObjectCreated(object created)
    {
      ObjectCreated?.Invoke(created);
    }

    public BoDiContainerProxy(FlexDi.IContainer proxiedContainer)
    {
      if(proxiedContainer == null)
        throw new ArgumentNullException(nameof(proxiedContainer));
      this.proxiedContainer = proxiedContainer;

      proxiedContainer.AddRegistrations(r => r.RegisterInstance(this).As<IObjectContainer>());

      proxiedContainer.ServiceResolved += (sender, e) => {
        if(e.Registration is TypeRegistration)
          InvokeObjectCreated(e.Instance);
      };
    }
  }
}
