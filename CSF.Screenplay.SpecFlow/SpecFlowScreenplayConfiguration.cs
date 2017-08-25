using System;
using System.Configuration;
using CSF.Configuration;

namespace CSF.Screenplay.SpecFlow
{
  /// <summary>
  /// Configuration section for the SpecFlow/Screenplay integration.
  /// </summary>
  [ConfigurationPath("SpecFlowScreenplay")]
  public class SpecFlowScreenplayConfiguration : ConfigurationSection, IScreenplayConfiguration
  {
    const string IntegrationAssemblyQualifiedNameConfigName = @"IntegrationConfigurationTypeName";

    /// <summary>
    /// Gets the assembly-qualified type name for the Screenplay Integration Configuration class to be used.
    /// </summary>
    /// <value>The assembly qualified name of the integration configuration type.</value>
    [ConfigurationProperty(IntegrationAssemblyQualifiedNameConfigName, IsRequired = true)]
    public virtual string IntegrationConfigTypeName
    {
      get { return (string) this[IntegrationAssemblyQualifiedNameConfigName]; }
      set { this[IntegrationAssemblyQualifiedNameConfigName] = value; }
    }

    public Type GetIntegrationConfigType()
    {

      var integrationConfigTypeName = IntegrationConfigTypeName;
      if(integrationConfigTypeName == null)
      {
        var message = "The SpecFlow/Screenplay configuration must specify an assembly qualified type for the integration.";
        throw new InvalidOperationException(message);
      }

      var configType = Type.GetType(integrationConfigTypeName);
      if(configType == null)
      {
        var message = $"The Screenplay integration configuration type '{configType}' was not found.";
        throw new InvalidOperationException(message);
      }

      return configType;
    }
  }
}
