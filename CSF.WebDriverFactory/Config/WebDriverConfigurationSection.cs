using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using CSF.Configuration;

namespace CSF.WebDriverFactory.Config
{
  /// <summary>
  /// Implementation of <see cref="IWebDriverFactoryConfiguration"/> which makes use of a <c>ConfigurationSection</c>
  /// to store the information.
  /// </summary>
  [ConfigurationPath("WebDriverFactory")]
  public class WebDriverConfigurationSection : ConfigurationSection, IWebDriverFactoryConfiguration
  {
    const string
      FactoryTypeConfigName = @"FactoryType",
      FactoryPropertiesConfigName = @"FactoryProperties";

    /// <summary>
    /// Gets or sets the assembly-qualified type name of the factory implementation to use.
    /// </summary>
    /// <value>The factory type.</value>
    [ConfigurationProperty(FactoryTypeConfigName, IsRequired = true)]
    public virtual string FactoryType
    {
      get { return (string) this[FactoryTypeConfigName]; }
      set { this[FactoryTypeConfigName] = value; }
    }

    /// <summary>
    /// Gets or sets a collection of the <see cref="FactoryProperty"/> instances which should be applied to the
    /// factory.
    /// </summary>
    /// <value>The factory properties.</value>
    [ConfigurationProperty(FactoryPropertiesConfigName, IsRequired = false)]
    public virtual FactoryPropertyElementCollection FactoryProperties
    {
      get { return (FactoryPropertyElementCollection) this[FactoryPropertiesConfigName]; }
      set { this[FactoryPropertiesConfigName] = value; }
    }

    /// <summary>
    /// Gets the <c>System.Type</c> of web driver factory desired.
    /// </summary>
    /// <returns>The factory type.</returns>
    public Type GetFactoryType()
    {
      if(FactoryType == null)
        return null;

      return Type.GetType(FactoryType);
    }

    /// <summary>
    /// Gets a collection of name/value pairs which indicate public settable properties on the factory instance
    /// and values to set into them.
    /// </summary>
    /// <returns>The factory properties.</returns>
    public IDictionary<string, string> GetFactoryProperties()
    {
      if(FactoryProperties == null)
        return new Dictionary<string,string>();

      return FactoryProperties
        .Cast<FactoryProperty>()
        .ToDictionary(k => k.Name, v => v.Value);
    }
  }
}
