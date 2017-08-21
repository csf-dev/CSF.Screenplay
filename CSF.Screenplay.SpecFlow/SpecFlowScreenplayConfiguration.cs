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
    const string IntegrationAssemblyQualifiedNameConfigName = @"IntegrationAssemblyQualifiedName";

    /// <summary>
    /// Gets the assembly-qualified type name for the Screenplay Integration class to be used.
    /// </summary>
    /// <value>The name of the integration assembly qualified.</value>
    [ConfigurationProperty(IntegrationAssemblyQualifiedNameConfigName, IsRequired = true)]
    public virtual string IntegrationAssemblyQualifiedName
    {
      get { return (string) this[IntegrationAssemblyQualifiedNameConfigName]; }
      set { this[IntegrationAssemblyQualifiedNameConfigName] = value; }
    }
  }
}
