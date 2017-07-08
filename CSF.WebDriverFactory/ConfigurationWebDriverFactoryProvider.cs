using System;
using System.Reflection;
using CSF.Configuration;
using CSF.WebDriverFactory.Config;

namespace CSF.WebDriverFactory
{
  /// <summary>
  /// Implementation of <see cref="IWebDriverFactoryProvider"/> which reads information from a configuration source
  /// and creates a web driver factory according to the information within.
  /// </summary>
  public class ConfigurationWebDriverFactoryProvider : IWebDriverFactoryProvider
  {
    const BindingFlags
      PropertySettingBindingFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase;
    static readonly Type BaseFactoryType = typeof(IWebDriverFactory);

    readonly IWebDriverFactoryConfiguration injectedConfig;

    /// <summary>
    /// Gets the web driver factory instance, using the configuration data.
    /// </summary>
    /// <returns>The factory.</returns>
    public virtual IWebDriverFactory GetFactory()
    {
      var config = GetConfiguration();
      var factory = GetFactory(config);
      ConfigureFactory(factory, config);
      return factory;
    }

    /// <summary>
    /// Gets the configuration information.
    /// </summary>
    /// <returns>The configuration.</returns>
    protected virtual IWebDriverFactoryConfiguration GetConfiguration()
    {
      if(injectedConfig != null)
        return injectedConfig;

      var reader = new ConfigurationReader();
      var config = reader.ReadSection<WebDriverConfigurationSection>();

      if(config == null)
        throw new InvalidOperationException("The configuration must not be null; please check your configuration file.");

      return config;
    }

    /// <summary>
    /// Creates and returns the web driver factory instance.
    /// </summary>
    /// <returns>The factory.</returns>
    /// <param name="config">Configuration.</param>
    protected virtual IWebDriverFactory GetFactory(IWebDriverFactoryConfiguration config)
    {
      if(config == null)
        throw new ArgumentNullException(nameof(config));

      var factoryType = config.GetFactoryType();
      if(factoryType == null)
        throw new ArgumentException($"The configuration must return a non-null factory type; check that the configured type exists.",
                                    nameof(config));
      if(!BaseFactoryType.IsAssignableFrom(factoryType))
        throw new ArgumentException($"A factory type must implement `{BaseFactoryType.Name}', but `{factoryType.FullName}' does not.",
                                    nameof(config));

      return (IWebDriverFactory) Activator.CreateInstance(factoryType);
    }

    /// <summary>
    /// Configures the factory by setting property values according to information from the configuration.
    /// </summary>
    /// <param name="factory">Factory.</param>
    /// <param name="config">Config.</param>
    protected virtual void ConfigureFactory(IWebDriverFactory factory, IWebDriverFactoryConfiguration config)
    {
      if(factory == null)
        throw new ArgumentNullException(nameof(factory));
      if(config == null)
        throw new ArgumentNullException(nameof(config));

      var factoryType = factory.GetType();
      var propertiesToSet = config.GetFactoryProperties();

      foreach(var propertyName in propertiesToSet.Keys)
      {
        var propertyValue = propertiesToSet[propertyName];
        SetFactoryPropertyValue(factoryType, factory, propertyName, propertyValue);
      }
    }

    /// <summary>
    /// Sets a single property value on the given factory instance.
    /// </summary>
    /// <param name="type">Type.</param>
    /// <param name="instance">Instance.</param>
    /// <param name="propertyName">Property name.</param>
    /// <param name="propertyValue">Property value.</param>
    protected virtual void SetFactoryPropertyValue(Type type, object instance, string propertyName, string propertyValue)
    {
      var property = type.GetProperty(propertyName, PropertySettingBindingFlags);
      if(property == null)
        throw new ArgumentException($"There must be a public instance property on `{type.FullName}' named '{propertyName}'.",
                                    propertyName);

      if(!property.CanWrite)
        throw new ArgumentException($"The property {type.Name}.{property.Name} must be settable.", nameof(propertyName));

      var convertedValue = Convert.ChangeType(propertyValue, property.PropertyType);
      property.SetValue(instance, convertedValue);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ConfigurationWebDriverFactoryProvider"/> class.
    /// </summary>
    public ConfigurationWebDriverFactoryProvider() : this(null) {}

    /// <summary>
    /// Initializes a new instance of the <see cref="ConfigurationWebDriverFactoryProvider"/> class.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Generally, callers do not need to use this overload of the constructor, as the default behaviour is to read the
    /// configuration information from the XML configuration.
    /// </para>
    /// <para>
    /// Only use this constructor if you need to inject the configuration from an alternative source.
    /// </para>
    /// </remarks>
    /// <param name="injectedConfig">An instance of <see cref="IWebDriverFactoryConfiguration"/> to inject into the factory.</param>
    public ConfigurationWebDriverFactoryProvider(IWebDriverFactoryConfiguration injectedConfig)
    {
      this.injectedConfig = injectedConfig;
    }
  }
}
