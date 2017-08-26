using System;
namespace CSF.Screenplay.Integration
{
  /// <summary>
  /// Factory type for the creation of a sceeenplay integration.
  /// </summary>
  public class IntegrationFactory
  {
    /// <summary>
    /// Static factory method which creates a new implementation of <see cref="IScreenplayIntegration"/>
    /// from a given configuration type.
    /// </summary>
    /// <param name="configType">Config type.</param>
    public IScreenplayIntegration Create(Type configType)
    {
      var config = GetConfig(configType);
      return new ScreenplayIntegration(config);
    }

    IIntegrationConfig GetConfig(Type configType)
    {
      if(configType == null)
        throw new ArgumentNullException(nameof(configType));

      if(!typeof(IIntegrationConfig).IsAssignableFrom(configType))
      {
        throw new ArgumentException($"Configuration type must implement `{typeof(IIntegrationConfig).Name}'.",
                                    nameof(configType));
      }

      return (IIntegrationConfig) Activator.CreateInstance(configType);
    }
  }
}
