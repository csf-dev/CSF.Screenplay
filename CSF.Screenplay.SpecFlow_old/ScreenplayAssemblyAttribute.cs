﻿using System;
using CSF.Screenplay.Integration;

namespace CSF.Screenplay.SpecFlow
{
  /// <summary>
  /// Indicates that the assembly contains Screenplay tests.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
  public class ScreenplayAssemblyAttribute : Attribute
  {
    static Lazy<IScreenplayIntegration> integration;

    /// <summary>
    /// Gets the current Screenplay integration.
    /// </summary>
    /// <value>The integration.</value>
    public IScreenplayIntegration Integration => integration.Value;

    /// <summary>
    /// Initializes a new instance of the <see cref="ScreenplayAssemblyAttribute"/> class.
    /// </summary>
    /// <param name="configType">Integration type.</param>
    public ScreenplayAssemblyAttribute(Type configType)
    {
      integration = integration?? new Lazy<IScreenplayIntegration>(() => CreateIntegration(configType));
    }

    IScreenplayIntegration CreateIntegration(Type configType)
    {
      var rootContainer = FlexDi.Container
        .CreateBuilder()
        .DoNotMakeAllResolutionOptional()
        .ResolveUnregisteredTypes()
        .SelfRegisterAResolver()
        .SelfRegisterTheRegistry()
        .SupportResolvingLazyInstances()
        .DoNotSupportResolvingNamedInstanceDictionaries()
        .ThrowOnCircularDependencies()
        .UseInstanceCache()
        .DoNotUseNonPublicConstructors()
        .Build();

      return new IntegrationFactory().Create(configType, rootContainer);
    }
  }
}
