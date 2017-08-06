using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace CSF.Screenplay.Context
{
  /// <summary>
  /// The screenplay context type, which acts as a service locator for the various services.
  /// Please note that all functionality is implemented as explicit interface implementations.  Please use extension
  /// methods to register and get the services themselves.
  /// </summary>
  public class ScreenplayContext : IScreenplayContext
  {
    readonly IDictionary<ServiceRegistration,object> singletonServices;
    readonly IDictionary<ServiceRegistration,Func<object>> perScenarioServices;

    TService IScreenplayContext.GetService<TService>(string name)
    {
      return GetPerScenarioService<TService>(name)?? GetSingletonService<TService>(name);
    }

    void IScreenplayContext.RegisterPerScenario<TService>(string name)
    {
      Func<TService> factory = () => Activator.CreateInstance<TService>();
      RegisterPerScenario(typeof(TService), factory, name);
    }

    void IScreenplayContext.RegisterPerScenario<TService>(Func<TService> factory, string name)
    {
      RegisterPerScenario(typeof(TService), factory, name);
    }

    void IScreenplayContext.RegisterSingleton<TService>(TService instance, string name)
    {
      if(instance == null)
        throw new ArgumentNullException(nameof(instance));
      
      var reg = CreateRegistration(typeof(TService), name, ServiceLifetime.PerTestRun);
      singletonServices.Add(reg, instance);
    }

    TService GetPerScenarioService<TService>(string name) where TService : class
    {
      var desiredReg = CreateRegistration(typeof(TService), name, ServiceLifetime.PerScenario);
      var isRegistered = perScenarioServices.ContainsKey(desiredReg);

      if(!isRegistered)
        return null;

      var factory = perScenarioServices[desiredReg];
      return (TService) factory();
    }

    TService GetSingletonService<TService>(string name) where TService : class
    {
      var desiredReg = CreateRegistration(typeof(TService), name, ServiceLifetime.PerTestRun);
      var isRegistered = singletonServices.ContainsKey(desiredReg);

      if(!isRegistered)
        return null;

      return (TService) singletonServices[desiredReg];
    }

    void RegisterPerScenario(Type type, Func<object> factory, string name)
    {
      if(type == null)
        throw new ArgumentNullException(nameof(type));
      if(factory == null)
        throw new ArgumentNullException(nameof(factory));
      
      var reg = CreateRegistration(type, name, ServiceLifetime.PerScenario);
      perScenarioServices.Add(reg, factory);
    }

    ServiceRegistration CreateRegistration(Type type, string name, ServiceLifetime lifetime)
    {
      return new ServiceRegistration(type, name, lifetime);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Context.ScreenplayContext"/> class.
    /// </summary>
    public ScreenplayContext()
    {
      singletonServices = new ConcurrentDictionary<ServiceRegistration,object>();
      perScenarioServices = new ConcurrentDictionary<ServiceRegistration,Func<object>>();
    }

    #region singleton

    static ScreenplayContext current;

    /// <summary>
    /// Gets the current <see cref="ScreenplayContext"/>.
    /// </summary>
    /// <value>The current context.</value>
    public static ScreenplayContext Current => current;

    /// <summary>
    /// Initializes the <see cref="T:CSF.Screenplay.Context.ScreenplayContext"/> class.
    /// </summary>
    static ScreenplayContext()
    {
      current = new ScreenplayContext();
    }

    #endregion
  }
}
