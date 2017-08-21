using System;
using System.Configuration;
using CSF.Configuration;

namespace CSF.Screenplay.SpecFlow
{
  [ConfigurationPath("SpecFlowScreenplay")]
  public class SpecFlowScreenplayConfiguration : ConfigurationSection, IScreenplayConfiguration
  {
    const string IntegrationAssemblyQualifiedNameConfigName = @"IntegrationAssemblyQualifiedName";
    [ConfigurationProperty(IntegrationAssemblyQualifiedNameConfigName, IsRequired = true)]
    public virtual string IntegrationAssemblyQualifiedName
    {
      get { return (string) this[IntegrationAssemblyQualifiedNameConfigName]; }
      set { this[IntegrationAssemblyQualifiedNameConfigName] = value; }
    }
  }
}
