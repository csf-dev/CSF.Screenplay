﻿using System;
using CSF.Screenplay.Scenarios;

namespace CSF.Screenplay.Integration
{
  /// <summary>
  /// Factory type for the creation of a sceeenplay integration.
  /// </summary>
  public class IntegrationFactory
  {
    /// <summary>
    /// Factory method which creates a new implementation of <see cref="IScreenplayIntegration"/>
    /// from a given configuration type.
    /// </summary>
    /// <param name="configType">The <c>System.Type</c> of the implementation of <see cref="IIntegrationConfig"/> to use.</param>
    /// <param name="rootContainer">An optional mechanism by which to pass the root FlexDi container to the integration.</param>
    public IScreenplayIntegration Create(Type configType, FlexDi.IContainer rootContainer = null)
    {
      var config = GetConfig(configType);
      var builder = GetBuilder(config);

      return new ScreenplayIntegration(builder, rootContainer);
    }

    IIntegrationConfig GetConfig(Type configType)
    {
      if(configType == null)
        throw new ArgumentNullException(nameof(configType));

      if(!typeof(IIntegrationConfig).IsAssignableFrom(configType))
      {
        var message = String.Format(Resources.ExceptionFormats.IntegrationConfigTypeMustImplementCorrectInterface,
                                    typeof(IIntegrationConfig).Name);
        throw new ArgumentException(message, nameof(configType));
      }

      return (IIntegrationConfig) Activator.CreateInstance(configType);
    }

    IIntegrationConfigBuilder GetBuilder(IIntegrationConfig config)
    {
      var output = new IntegrationConfigurationBuilder();
      AddBaselineIntegrationConfiguration(output);
      config.Configure(output);
      return output;
    }

    void AddBaselineIntegrationConfiguration(IIntegrationConfigBuilder builder)
    {
      builder.UseCast();
      builder.UseStage();
    }
  }
}
